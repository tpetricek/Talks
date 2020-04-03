var fs = require("fs");
var path = require("path");
var HtmlWebpackPlugin = require('html-webpack-plugin');
var webpack = require("webpack");

var packageJson = JSON.parse(fs.readFileSync(resolve('../package.json')).toString());
var errorMsg = "{0} missing in package.json";

var config = {
  entry: { 
    "app": "../src/main.ts",
    // "editor.worker": 'monaco-editor/esm/vs/editor/editor.worker.js',
  },
  publicDir: resolve("../public"),
  buildDir: resolve("../build"),
  nodeModulesDir: resolve("../node_modules"),
  indexHtmlTemplate: resolve("../public/index.html")
}

function resolve(filePath) {
  return path.join(__dirname, filePath)
}

function forceGet(obj, path, errorMsg) {
  function forceGetInner(obj, head, tail) {
    if (head in obj) {
      var res = obj[head];
      return tail.length > 0 ? forceGetInner(res, tail[0], tail.slice(1)) : res;
    }
    throw new Error(errorMsg.replace("{0}", path));
  }
  var parts = path.split('.');
  return forceGetInner(obj, parts[0], parts.slice(1));
}

function getModuleRules(isProduction) {
  var babelOptions = {
    presets: [
      ["env", { "targets": { "browsers": "> 1%" }, "modules": false }]
    ],
  };

  return [
    { 
      test: /\.ts|\.tsx?$/, 
      loader: "ts-loader" 
    },
    {
      test: /\.js$/,
      exclude: /node_modules/,
      use: {
        loader: 'babel-loader',
        options: babelOptions
      },
    },
    {
      test: /\.css$/,
      use: ['style-loader', 'css-loader'],
    }
  ];
}

function getPlugins(isProduction) {
  return [
    new HtmlWebpackPlugin({
      filename: path.join(config.buildDir, "index.html"),
      template: config.indexHtmlTemplate,
      minify: false //isProduction ? {} : false
    }),
    new webpack.NamedModulesPlugin(),
    new webpack.ContextReplacementPlugin(
      /monaco-editor(\\|\/)esm(\\|\/)vs(\\|\/)editor(\\|\/)common(\\|\/)services/,
      __dirname
    )
  ];
}

module.exports = {
  resolve: resolve,
  config: config,
  getModuleRules: getModuleRules,
  getPlugins: getPlugins
}
