/**
 * @type {import('vite').UserConfig}
 */
const config = {
    // ...
    root: './public',
    build: {
        emptyOutDir: true,
        outDir: '../dist'
    }
};

export default config;