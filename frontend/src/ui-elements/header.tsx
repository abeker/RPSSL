import { Toolbar, Typography } from "@mui/material";
import { useTheme } from '@mui/material/styles';

const Header = () => {
    const theme = useTheme();

    return (
        <Toolbar>
            <Typography variant="h3" style={{ flexGrow: 1, color: theme.palette.primary.main }}>
                Rock Paper Scissors Lizard Spock
            </Typography>
        </Toolbar>
    );
};

export default Header;
