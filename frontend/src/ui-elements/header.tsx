import { Toolbar, Typography, Button } from "@mui/material";
import { useTheme } from '@mui/material/styles';

interface HeaderProps {
    onLogout: () => void;
    isUserLoggedIn: boolean;
}

const Header = ({ onLogout, isUserLoggedIn }: HeaderProps) => {
    const theme = useTheme();

    return (
        <Toolbar sx={{ display: 'flex', alignItems: 'center', width: '100%', gap: '16px' }}>
            <Typography
                variant="h3"
                style={{ color: theme.palette.primary.main, flexGrow: 1 }}
            >
                Rock Paper Scissors Lizard Spock
            </Typography>
            {isUserLoggedIn && (
                <Button
                    color="primary"
                    onClick={onLogout}
                    variant="outlined"
                    sx={{
                        border: `2px solid ${theme.palette.primary.main}`,
                        mt: '8px',
                    }}
                >
                    Logout
                </Button>
            )}
        </Toolbar>
    );
};

export default Header;
