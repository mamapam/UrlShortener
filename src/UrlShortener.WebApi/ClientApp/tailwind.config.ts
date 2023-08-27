// /** @type {import('tailwindcss').Config} */
// module.exports = {
//   content: ['./index.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
//   theme: {
//     extend: {}
//   },
//   plugins: []
// }

import type { Config } from 'tailwindcss'

export default {
  content: ['./index.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
  theme: {
    extend: {
      colors: {
        primary: '#9954F0',
        secondary: '#6E6A6A',
      },
      screens: {
        xs: '275px',
        sm: '640px',
        md: '768px',
        lg: '1024px',
        xl: '1280px',
        '2xl': '1536px',
      },
      spacing: {
        200: '75rem',
      },
    },
  },
  plugins: [],
} satisfies Config
