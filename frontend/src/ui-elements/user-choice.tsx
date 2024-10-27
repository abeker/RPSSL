import { Box, Typography } from '@mui/material';
import { Choice } from '../models/choice';

interface UserChoiceProps {
    userChoice: Choice | null;
}

const UserChoice = ({ userChoice }: UserChoiceProps) => (
    <Box style={{ flex: 1, textAlign: 'center', color: 'dodgerBlue'}}>
        <Typography variant="h4">Your choice</Typography>
        <Typography variant="h5">{userChoice ? userChoice.name : 'None'}</Typography>
    </Box>
);

export default UserChoice;
