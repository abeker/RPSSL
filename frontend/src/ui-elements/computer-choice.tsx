import { Box, Typography, CircularProgress } from '@mui/material';
import { Choice } from '../models/choice';

interface ComputerChoiceProps {
    computerChoice: Choice | null;
    loading: boolean;
}

const ComputerChoice = ({ computerChoice, loading }: ComputerChoiceProps) => (
    <Box style={{ flex: 1, textAlign: 'center' }}>
        <Typography variant="h6">Computer Choice:</Typography>
        {loading ? (
            <CircularProgress />
        ) : (
            <Typography variant="h5">{computerChoice ? computerChoice.name : 'None'}</Typography>
        )}
    </Box>
);

export default ComputerChoice;