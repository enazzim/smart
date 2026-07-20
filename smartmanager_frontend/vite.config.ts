import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

// 운영(EC2): http://<host>/smartmanager/
// 로컬 dev: http://localhost:5173/
export default defineConfig(({ mode }) => ({
  base: mode === 'production' ? '/smartmanager/' : '/',
  plugins: [react()],
  define: {
    global: 'globalThis',
  },
  server: {
    port: 5173,
    proxy: {
      '/api': {
        target: 'http://localhost:8080',
        changeOrigin: true,
      },
      '/ws-drawing': {
        target: 'http://localhost:8080',
        changeOrigin: true,
        ws: true,
      },
    },
  },
}));
