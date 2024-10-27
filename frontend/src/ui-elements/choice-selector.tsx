import { Button } from '@mui/material';
import { Choice } from '../models/choice';

interface ChoiceSelectorProps {
    choices: Choice[] | null;
    onChoiceSelect: (choice: Choice) => void;
}

const ChoiceSelector = ({ choices, onChoiceSelect }: ChoiceSelectorProps) => (
    <div style={{ display: 'flex', justifyContent: 'space-around', marginTop: '20px' }}>
        {choices?.map((choice) => (
            <Button
                key={choice.id}
                variant="contained"
                color="primary"
                style={{
                    flex: 1,
                    margin: '0 5px',
                    borderRadius: '20px',
                    boxShadow: '0 4px 8px rgba(0,0,0,0.2)',
                }}
                onClick={() => onChoiceSelect(choice)}
            >
                {choice.name}
            </Button>
        ))}
    </div>
);

export default ChoiceSelector;
