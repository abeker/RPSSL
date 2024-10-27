import { Button, Typography } from "@mui/material";
import { Choice } from "../models/choice";

interface ChoiceSelectorProps {
  choices: Choice[] | null;
  onChoiceSelect: (choice: Choice) => void;
}

const ChoiceSelector = ({ choices, onChoiceSelect }: ChoiceSelectorProps) => (
  <div
    style={{
      display: "flex",
      justifyContent: "space-around",
      marginTop: "20px",
      gap: '10px',
    }}
  >
    {choices?.map((choice) => (
      <Button
        key={choice.id}
        variant="contained"
        color="primary"
        sx={{
          display: 'flex',
          textAlign: 'center',
          borderRadius: "10px",
          boxShadow: "0 4px 8px rgba(0, 0, 0, 0.2)",
          backgroundColor: (theme) => theme.palette.primary.main,
          color: (theme) => theme.palette.common.white,
          textTransform: "uppercase",
          '&:hover': {
            backgroundColor: (theme) => theme.palette.primary.dark,
          },
        }}
        onClick={() => onChoiceSelect(choice)}
      >
        <Typography variant="h5" component="span">
          {choice.name}
        </Typography>
      </Button>
    ))}
  </div>
);

export default ChoiceSelector;
