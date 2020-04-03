var path = require("path");
var webpack = require("webpack");
var common = require("./webpack.config.common");

console.log("Bundling for development...");

module.exports = {
  devtool: "source-map",
  entry: common.config.entry,
  mode: "development",
  node: {
    fs: 'empty'
  },
  output: {
    filename: '[name].js',
    path: common.config.buildDir,
    devtoolModuleFilenameTemplate: info =>
      path.resolve(info.absoluteResourcePath).replace(/\\/g, '/'),
  },
  devServer: {
    contentBase: common.config.publicDir,
    publicPath: '/',
    host: '0.0.0.0',
    port: 7070,
    hot: true,
    inline: true,
    headers: {
      "Access-Control-Allow-Origin": "*",
      "Access-Control-Allow-Methods": "GET, POST, PUT, DELETE, PATCH, OPTIONS",
      "Access-Control-Allow-Headers": "X-Requested-With, content-type, Authorization"
    }
  },
  module: {
    rules: common.getModuleRules()
  },
  plugins: common.getPlugins().concat([
      new webpack.HotModuleReplacementPlugin()
  ]),
  resolve: {
    modules: [common.config.nodeModulesDir],
    extensions: [".ts", ".tsx", ".js"]
  },
};
