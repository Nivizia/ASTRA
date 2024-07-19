import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react-swc'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  optimizeDeps: {
    include: ['@mui/material/Tooltip', '@emotion/styled', '@mui/material/Box'],
  },
  server: {
    host: 'astradiamonds.com',
    port: 5173,
    proxy: {
      '/api': {
        target: 'http://astradiamonds.com',
        changeOrigin: true,
        secure: false
      }
    }
  },
});