<script lang="ts" type="module">
	import { onMount } from "svelte";
	import * as monaco from "monaco-editor";

	let editorContainer: HTMLDivElement;
	let previewContainer: HTMLDivElement;

	let codeEditor: monaco.editor.IStandaloneCodeEditor;
	let previewEditor: monaco.editor.IStandaloneCodeEditor;

	let throttleHandle: NodeJS.Timeout = undefined;
	function OnContentChanged() {
		if (throttleHandle) clearTimeout(throttleHandle);

		throttleHandle = setTimeout(async () => {
			const code = codeEditor.getValue();
			try {
				const result = await fetch("api/v1.0/decompile", {
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
</script>

<main>
	<div>
		<div
			class="container"
			bind:this={editorContainer}
			style="grid-column: 1;"
		/>
	</div>
	<div>
		<div
			class="container"
			bind:this={previewContainer}
			style="grid-column: 2;"
		/>
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
</style>
