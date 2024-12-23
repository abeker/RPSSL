import { Typography, Link } from '@mui/material';
import { makeStyles } from '@mui/styles';
import { useTheme } from '@mui/material/styles';
import { Link as RouterLink } from 'react-router-dom';

interface GameResultProps {
    result: string | null;
}

const useStyles = makeStyles(() => ({
    blinkingText: {
        animation: '$blink 1.5s infinite',
    },
    '@keyframes blink': {
        '0%': { opacity: 1 },
        '50%': { opacity: 0 },
        '100%': { opacity: 1 },
    },
}));

const GameResult = ({ result }: GameResultProps) => {
    const classes = useStyles();
    const theme = useTheme();

    return (
        <>
            <Typography
                variant="h3"
                className={classes.blinkingText}
                style={{
                    marginTop: '20px',
                    color: theme.palette.secondary.main
                }}
            >
                {getResultMessage(result)}
            </Typography>

            {result && (
                <Link
                    component={RouterLink}
                    to="/scoreboard"
                    variant="body1"
                    style={{
                        marginTop: '10px',
                        display: 'block',
                        color: theme.palette.primary.main
                    }}
                >
                    View scoreboard
                </Link>
            )}
        </>
    );
};

const getResultMessage = (result: string | null): string => {
    switch (result) {
        case 'win':
            return 'You Win!';
        case 'lose':
            return 'You Lose!';
        case 'tie':
            return "It's a Tie!";
        default:
            return '';
    }
};

export default GameResult;
