import { createTheme } from '@mui/material/styles';

const primary = '#FFA500';
const primaryDark = '#CC8400';
const secondary= '#FF8C00';
const white = '#FFFFFF';

const theme = createTheme({
  palette: {
    primary: {
      main: primary,
      dark: primaryDark
    },
    secondary: {
      main: secondary,
    },
    text: {
      primary: white
    },
  },
  typography: {
    fontFamily: 'Silkscreen, sans-serif',
  },
  components: {
    MuiTextField: {
      styleOverrides: {
        root: {
          marginBottom: '20px',
          '& .MuiOutlinedInput-root': {
            '& fieldset': {
              borderColor: primary,
            },
            '&:hover fieldset': {
              borderColor: primaryDark,
            },
            '&.Mui-focused fieldset': {
              borderColor: primary,
            },
          },
          '& .MuiInputBase-input': {
            textAlign: 'center',
            color: primary,
          },
          '& .MuiInputBase-input::placeholder': {
            textAlign: 'center',
            color: primary,
          },
        },
      },
    },
  },
});

export default theme;
