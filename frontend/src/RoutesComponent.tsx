import { Routes, Route } from "react-router-dom";
import { Choice } from "./models/choice";
import { Result } from "./models/result";
import GamePage from "./pages/game-page";
import LoginPage from "./pages/login-page";
import Scoreboard from "./pages/scoreboard";

interface RoutesComponentProps {
  isUserLoggedIn: boolean;
  username: string;
  choices: Choice[] | null;
  userChoice: Choice | null;
  computerChoice: Choice | null;
  gameResult: Result | null;
  loading: boolean;
  errorMessage: string;
  onLogin: (username: string) => void;
  onChoiceSelect: (choice: Choice) => void;
}

function RoutesComponent({
  isUserLoggedIn,
  username,
  choices,
  userChoice,
  computerChoice,
  gameResult,
  loading,
  errorMessage,
  onLogin,
  onChoiceSelect,
}: RoutesComponentProps) {
  return (
    <Routes>
      <Route
        path="/"
        element={
          isUserLoggedIn ? (
            <GamePage
              userChoice={userChoice}
              computerChoice={computerChoice}
              gameResult={gameResult}
              username={username}
              choices={choices}
              loading={loading}
              onChoiceSelect={onChoiceSelect}
            />
          ) : (
            <LoginPage onLogin={onLogin} errorMessage={errorMessage} />
          )
        }
      />
      <Route path="/scoreboard" element={<Scoreboard index={0} size={5}/>} />
    </Routes>
  );
}

export default RoutesComponent;
