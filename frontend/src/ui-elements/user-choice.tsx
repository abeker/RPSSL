import { Box, Typography } from '@mui/material';
import { Choice } from '../models/choice';

interface UserChoiceProps {
    userChoice: Choice | null;
    username: string;
}

const UserChoice = ({ userChoice, username }: UserChoiceProps) => (
    <Box style={{ flex: 1, textAlign: 'center', color: 'dodgerBlue'}}>
        <Typography variant="h4">{username}'s choice</Typography>
        <Typography variant="h5">{userChoice ? userChoice.name : 'None'}</Typography>
    </Box>
);

export default UserChoice;
