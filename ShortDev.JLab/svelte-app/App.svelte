<script lang="ts" type="module">
	import type { JLabDiagnostic, JLabResult } from "./Api";
	import { JLabDiagnosticType } from "./Api";
	type JLabOutputType = "JavaCode" | "ByteCode" | "ByteCode-Verbose" | "Run";

	import { onMount } from "svelte";
	import * as monaco from "monaco-editor";

	let editorContainer: HTMLDivElement;
	let previewContainer: HTMLDivElement;

	let codeEditor: monaco.editor.IStandaloneCodeEditor;
	let previewEditor: monaco.editor.IStandaloneCodeEditor;

	let outputType: JLabOutputType = "JavaCode";

	let throttleHandle: NodeJS.Timeout = undefined;
	function OnContentChanged() {
		if (throttleHandle) clearTimeout(throttleHandle);

		const progressRing = document.querySelector(
			"#progressRing"
		) as HTMLElement;
		progressRing.style.display = "block";

		throttleHandle = setTimeout(async () => {
			const code = codeEditor.getValue();
			try {
				console.log(outputType);
				const result: JLabResult = await fetch(
					`api/v1.0/${outputType}`,
					{
						method: "post",
						headers: {
							"content-type": "text/plain",
						},
						body: code,
					}
				).then((x) => x.json());

				const dignosticData = JSON.parse(
					result.diagnostics
				) as JLabDiagnostic[];

				const model = codeEditor.getModel();
				const markerData = dignosticData.map((diagnostic) => {
					const { lineNumber: startLineNumber, column: startColumn } =
						model.getPositionAt(diagnostic.start);
					const { lineNumber: endLineNumber, column: endColumn } =
						model.getPositionAt(diagnostic.end);
					return {
						message: diagnostic.msg,
						severity: getMonacoSeverity(),
						startLineNumber,
						startColumn,
						endLineNumber,
						endColumn,
					} as monaco.editor.IMarkerData;

					function getMonacoSeverity() {
						switch (diagnostic.kind) {
							case JLabDiagnosticType.Error:
								return monaco.MarkerSeverity.Error;
							case JLabDiagnosticType.Warning:
							case JLabDiagnosticType.MandatoryWarning:
								return monaco.MarkerSeverity.Warning;
							case JLabDiagnosticType.Note:
								return monaco.MarkerSeverity.Info;
						}
						return monaco.MarkerSeverity.Hint;
					}
				});
				monaco.editor.setModelMarkers(model, "owner", markerData);

				previewEditor.getModel().setValue(result.content);
			} catch (ex) {
				previewEditor.setValue(ex);
			}
			progressRing.style.display = "none";
		}, 500);
	}

	import opCodes from "./java_opcodes.json"; // assert { type: "json" };

	onMount(() => {
		const defaultValue = `public final class Test{

	public static void main(String[] args){
		System.out.println("Hello World!");
	}

}`;
		codeEditor = monaco.editor.create(editorContainer, {
			language: "java",
			automaticLayout: false,
			value: defaultValue,
		});
		codeEditor.onDidChangeModelContent((e) => OnContentChanged());

		monaco.languages.register({ id: "java" });

		previewEditor = monaco.editor.create(previewContainer, {
			language: "java",
			automaticLayout: false,
			readOnly: true,
			minimap: {
				enabled: false,
			},
		});

		monaco.languages.registerHoverProvider("java", {
			provideHover: function (model, position) {
				const wordAtPos = model.getWordAtPosition(position);
				const word = wordAtPos.word;
				const opCode = opCodes[word];
				console.log({ opCode });
				if (!opCode) return;

				return {
					range: new monaco.Range(
						position.lineNumber,
						wordAtPos.startColumn,
						position.lineNumber,
						wordAtPos.endColumn
					),
					contents: [
						{ value: "`" + word + "`" },
						{
							value: opCode.description,
						},
					],
				};
			},
		});

		// Theme
		{
			const updateTheme = (darkMode: boolean) => {
				const theme = darkMode ? "vs-dark" : "vs";
				codeEditor.updateOptions({ theme });
				previewEditor.updateOptions({ theme });
			};

			const mediaQuery = window.matchMedia(
				`(prefers-color-scheme: dark)`
			);
			mediaQuery.addEventListener("change", (e) => {
				updateTheme(e.matches);
			});
			updateTheme(mediaQuery.matches);
		}
	});

	window.addEventListener("resize", () => {
		codeEditor?.layout({} as monaco.editor.IDimension);
		codeEditor?.layout();

		previewEditor?.layout({} as monaco.editor.IDimension);
		previewEditor?.layout();
	});

	import {
		provideFluentDesignSystem,
		fluentCombobox,
		fluentOption,
		fluentProgressRing,
	} from "@fluentui/web-components";

	provideFluentDesignSystem().register(
		fluentCombobox(),
		fluentOption(),
		fluentProgressRing()
	);
</script>

<main>
	<div>
		<div
			class="container"
			bind:this={editorContainer}
			style="grid-column: 1;"
		/>
	</div>
	<div style="display: grid; grid-template-rows: auto 1fr;">
		<div class="toolbar">
			<fluent-combobox
				id="optionComboBox"
				placeholder="Output type"
				value={outputType}
				on:change={(e) => {
					outputType = e.target.value;
					OnContentChanged();
				}}
			>
				<fluent-option selected>JavaCode</fluent-option>
				<fluent-option>ByteCode</fluent-option>
				<fluent-option>ByteCode-Verbose</fluent-option>
				<fluent-option>Run</fluent-option>
			</fluent-combobox>
		</div>
		<div class="toolbody overlayContainer">
			<div
				class="container"
				bind:this={previewContainer}
				style="grid-column: 2;"
			/>
			<fluent-progress-ring
				id="progressRing"
				style="display: none;"
				class="overlayItem"
			/>
		</div>
	</div>
</main>

<style scoped>
	main {
		display: grid;
		grid-template-columns: 1fr 1fr;
	}
	main > * {
		flex-grow: 1;
	}

	.toolbar {
		grid-row: 1;
		padding: 10px;
	}

	.toolbody {
		grid-row: 2;
	}

	.overlayContainer {
		position: relative;
	}
	.overlayItem {
		position: absolute;
		top: 50%;
		left: 50%;
		transform: translate(-50%, -50%);
	}
</style>
