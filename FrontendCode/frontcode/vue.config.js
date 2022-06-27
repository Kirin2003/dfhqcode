const { defineConfig } = require("@vue/cli-service");
module.exports = defineConfig({
  transpileDependencies: true,
});
module.exports = {
  publicPath: "./",
  devServer: {
    proxy: {
      "/api": {
        target: "http://localhost:5001/api",
        changeOrigin: true,
        ws: true,
        secure: true,
        pathRewrite: {
          "^/api": "",
        },
      },
    },
  },
};
