const esbuild = require('esbuild');

esbuild.serve({
  servedir: '.',
  port: 3000,
}, {
  entryPoints: ['src/main.ts'],
  bundle: true,
  outfile: 'bundle.js',
  sourcemap: true,
  target: ['esnext'],
  watch: true
}).then(server => {
  console.log(`Server running at http://localhost:${server.port}`);
}).catch(() => process.exit(1));