var path = require("path");
var webpack = require("webpack");
var common = require("./webpack.config.common");

console.log("Bundling for development...");

module.exports = {
  devtool: "source-map",
  entry: common.config.entry,
  output: {
    filename: '[name].js',
    path: common.config.buildDir,
    devtoolModuleFilenameTemplate: info =>
      path.resolve(info.absoluteResourcePath).replace(/\\/g, '/'),
  },
  devServer: {
    contentBase: common.config.publicDir,
    publicPath: '/',
    port: 8080,
    hot: true,
    inline: true,
    // proxy
  },
  module: {
    rules: common.getModuleRules()
  },
  plugins: common.getPlugins().concat([
      new webpack.HotModuleReplacementPlugin(),
      new webpack.NamedModulesPlugin()
  ]),
  resolve: {
    modules: [common.config.nodeModulesDir]
  },
};
