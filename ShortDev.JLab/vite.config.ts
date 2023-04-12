import { defineConfig } from 'vite'
import { svelte } from '@sveltejs/vite-plugin-svelte'

// https://vitejs.dev/config/
export default defineConfig({
	build: {
		outDir: 'wwwroot/',
		emptyOutDir: false,
		minify: false
	},
	plugins: [svelte()]
})
