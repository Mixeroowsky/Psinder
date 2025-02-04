import { defineConfig } from "vite";
import plugin from "@vitejs/plugin-react";
import fs from "fs";
import path from "path";
import child_process from "child_process";
import { env } from "process";
import cookie from "cookie";

const getHttpsConfig = () => {
  if (process.env.NODE_ENV === "production") {
    return {
      key: process.env.KEY_PATH,
      cert: process.env.CERT_PATH,
    };
  } else {
    const baseFolder =
      env.APPDATA !== undefined && env.APPDATA !== ""
        ? `${env.APPDATA}/ASP.NET/https`
        : `${env.HOME}/.aspnet/https`;

    const certificateName = "psinder.client";
    const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
    const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

    if (!fs.existsSync(baseFolder)) {
      fs.mkdirSync(baseFolder, { recursive: true });
    }

    if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
      if (
        0 !==
        child_process.spawnSync(
          "dotnet",
          [
            "dev-certs",
            "https",
            "--export-path",
            certFilePath,
            "--format",
            "Pem",
            "--no-password",
          ],
          { stdio: "inherit" }
        ).status
      ) {
        throw new Error("Could not create certificate.");
      }
    }
    return {
      key: fs.readFileSync(keyFilePath),
      cert: fs.readFileSync(certFilePath),
    };
  }
};

export default defineConfig({
  plugins: [plugin()],
  resolve: {
    alias: {
      "@": path.resolve(__dirname, "./src"),
    },
  },
  server: {
    proxy: {
      "^/api": {
        target:
          process.env.NODE_ENV === "production"
            ? "https://stojek.art"
            : "https://localhost:7290",
        secure: false,
        changeOrigin: true,
        configure: (proxy) => {
          proxy.on("proxyReq", (proxyReq, req) => {
            const cookies = cookie.parse(req.headers.cookie || "");
            const token = `Bearer ${cookies.accessToken}`;
            if (token) {
              proxyReq.setHeader("Authorization", token);
            }
          });
        },
      },
    },
    port: 5173,
    https: getHttpsConfig(),
  },
  build: {
    outDir: "dist",
    sourcemap: process.env.NODE_ENV === "production" ? false : true,
  },
});
