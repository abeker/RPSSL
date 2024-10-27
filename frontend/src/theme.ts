import { createTheme } from '@mui/material/styles';

const theme = createTheme({
    palette: {
        primary: {
            main: '#FFA500',
            dark: '#CC8400'
        },
        secondary: {
            main: '#FF8C00',
        },
        common: {
            white: '#FFFFFF'
        },
    },
    typography: {
        fontFamily: "Silkscreen, sans-serif",
        h3: {
            fontFamily: "Silkscreen, sans-serif",
        },
        h4: {
            fontFamily: "Silkscreen, sans-serif",
        },
        h5: {
            fontFamily: "Silkscreen, sans-serif",
        },
    },
});

export default theme;
