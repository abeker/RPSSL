import { AppBar, Toolbar, Typography } from '@mui/material';

const Header = () => (
    <AppBar position="static" style={{ backgroundColor: 'rgba(255, 255, 255, 0.8)', color: 'black' }}>
        <Toolbar>
            <Typography variant="h6" style={{ flexGrow: 1 }}>
                Rock, Paper, Scissors, Lizard, Spock
            </Typography>
        </Toolbar>
    </AppBar>
);

export default Header;
