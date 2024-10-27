import { Typography } from '@mui/material';

interface GameResultProps {
    result: string | null;
}

const GameResult = ({ result }: GameResultProps) => (
    <Typography variant="h5" style={{ marginTop: '20px' }}>
        {getResultMessage(result)}
    </Typography>
);

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
