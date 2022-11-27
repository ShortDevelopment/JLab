<script lang="ts" type="module">
	type JLabOutputType = "JavaCode" | "ByteCode" | "ByteCode-Verbose";

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
				const result = await fetch(`api/v1.0/${outputType}`, {
					method: "post",
					headers: {
						"content-type": "text/plain",
					},
					body: code,
				}).then((x) => x.text());
				previewEditor.setValue(result);
			} catch (ex) {
				previewEditor.setValue(ex);
			}
			progressRing.style.display = "none";
		}, 1500);
	}

	onMount(() => {
		const defaultValue = `public final class Test{

	public static void Main(String[] args){
		
	}

}`;
		codeEditor = monaco.editor.create(editorContainer, {
			language: "java",
			automaticLayout: false,
			value: defaultValue,
		});
		codeEditor.onDidChangeModelContent((e) => OnContentChanged());

		previewEditor = monaco.editor.create(previewContainer, {
			language: "java",
			automaticLayout: false,
			readOnly: true,
			minimap: {
				enabled: false,
			},
		});
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
