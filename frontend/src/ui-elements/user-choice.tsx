import { Box, Typography } from '@mui/material';
import { Choice } from '../models/choice';

interface UserChoiceProps {
    userChoice: Choice | null;
}

const UserChoice = ({ userChoice }: UserChoiceProps) => (
    <Box style={{ flex: 1, textAlign: 'center' }}>
        <Typography variant="h6">Your Choice:</Typography>
        <Typography variant="h5">{userChoice ? userChoice.name : 'None'}</Typography>
    </Box>
);

export default UserChoice;
