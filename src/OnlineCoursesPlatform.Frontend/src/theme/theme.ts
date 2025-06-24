// src/theme/theme.ts
import { createTheme } from '@mui/material/styles';

// Вынесенные кастомные цвета для текста
export const textColors = {
  main: '#FFFFFF',
  muted: '#A5B1C2',
//   success: '#00FF99',
//   warning: '#FFC107',
//   danger: '#FF4444',
  link: '#8EC2FF',
//   note: '#CCCCCC',
//   info: '#5AC8FA',
//   code: '#FFD700',
//   label: '#7F8C8D',
};

export const theme = createTheme({
  palette: {
    primary: {
      main: '#6C63FF',
    },
    secondary: {
      main: '#FF8E16',
    },
    background: {
      default: '#161A1D', // Основной задний фон
      paper: '#1C252E',   // Вторичный фон
    },
    action: {
        hover: '#1C252E', // for: Drawer button(backgroundColor),
        selected: '#2A3746', // for: Drawer button(backgroundColor),
    },  
    
    text: {
      primary: textColors.muted,     // неактивный текст
      secondary: textColors.main,    // for: Appbar(text), Drawer(hover and active),
    },
    divider: '#33363E',
    error: {
      main: "#8B2C2C", // #9E3D3D или #A14242, 
      contrastText: "#fff",
    },
    link: {
    main: textColors.link,
    hover: '#61A6FA',
    },
    button: {
      main: '#0369A1',
      hover: '#0C85C6',
    },
    card: {
      background: '#1E2127',        // фон карточки
      text: '#FFFFFF',              // основной текст
      mutedText: '#A5B1C2',         // описание
      badgeText: "#000000",         // текст бейджа
      border: '#33363E',            // граница
      badgeNewBg: '#FF476C',        // бейдж NEW
      badgeProBg: '#697CF1',      // бейдж PRO
    },
  },
  typography: {
    fontFamily: '"Poppins", "Montserrat", "Inter", sans-serif',
  },
  
});
declare module '@mui/material/styles' {
  interface Palette {
    link: {
      main: string;
      hover?: string;
    };
    button: {
      main: string;
      hover: string;
    };
    card: {
      background: string;
      text: string;
      mutedText: string;
      badgeText: string;
      border: string;
      badgeNewBg: string;
      badgeProBg: string;
    };
  }

  interface PaletteOptions {
    link?: {
      main?: string;
      hover?: string;
    };
    button?: {
      main?: string;
      hover?: string;
    };
     card?: {
      background?: string;
      text?: string;
      mutedText?: string;
      badgeText?: string;
      border?: string;
      badgeNewBg?: string;
      badgeProBg?: string;
    };
  }
}