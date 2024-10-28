import { Box } from "@mui/material";
import UserChoice from "../ui-elements/user-choice";
import ComputerChoice from "../ui-elements/computer-choice";
import GameResult from "../ui-elements/game-result";
import ChoiceSelector from "../ui-elements/choice-selector";
import { Choice } from "../models/choice";
import { Result } from "../models/result";

interface GameScreenProps {
  userChoice: Choice | null;
  computerChoice: Choice | null;
  gameResult: Result | null;
  username: string;
  choices: Choice[] | null;
  loading: boolean;
  onChoiceSelect: (choice: Choice) => void;
}

function GamePage({
  userChoice,
  computerChoice,
  gameResult,
  username,
  choices,
  loading,
  onChoiceSelect,
}: GameScreenProps) {
  return (
    <>
      <Box style={{ display: "flex", justifyContent: "center", width: "100%" }}>
        <UserChoice userChoice={userChoice} username={username} />
        <ComputerChoice computerChoice={computerChoice} loading={loading} />
      </Box>
      <GameResult result={gameResult?.results || null} />
      <ChoiceSelector choices={choices} onChoiceSelect={onChoiceSelect} />
    </>
  );
}

export default GamePage;
