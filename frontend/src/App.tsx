import { useEffect, useState } from "react";
import { Box, Container, TextField, Button, ThemeProvider } from "@mui/material";
import {
  fetchChoices,
  playGame,
} from "./services/game-service";
import { createPlayer } from "./services/player-service";
import { Choice } from "./models/choice";
import { Result } from "./models/result";
import ChoiceSelector from "./ui-elements/choice-selector";
import Header from "./ui-elements/header";
import UserChoice from "./ui-elements/user-choice";
import ComputerChoice from "./ui-elements/computer-choice";
import GameResult from "./ui-elements/game-result";
import theme from "./theme";
import "./App.css";
import { getStoredUser, setUser, clearUser } from "./context/userContext";
import { AxiosError } from "axios";

function App() {
  const [choices, setChoices] = useState<Choice[] | null>(null);
  const [userChoice, setUserChoice] = useState<Choice | null>(null);
  const [computerChoice, setComputerChoice] = useState<Choice | null>(null);
  const [gameResult, setGameResult] = useState<Result | null>(null);
  const [loading, setLoading] = useState(false);
  const [username, setUsername] = useState<string>("");
  const [isUserLoggedIn, setIsUserLoggedIn] = useState<boolean>(false);
  const [errorMessage, setErrorMessage] = useState<string>("");

  useEffect(() => {
    const getChoices = async () => {
      const fetchedChoices = await fetchChoices();
      setChoices(fetchedChoices);
    };

    getChoices();
  }, []);

  useEffect(() => {
    const { username, isUserLoggedIn } = getStoredUser();
    setUsername(username);
    setIsUserLoggedIn(isUserLoggedIn);
  }, []);

  const handleUserChoice = async (choice: Choice) => {
    setUserChoice(choice);
    setLoading(true);
    setGameResult(null);

    const fetchedGameResult = await playGame(choice.id);
    setGameResult(fetchedGameResult);

    const computerChoiceIndex = fetchedGameResult.computer - 1;
    const computerChoice = choices?.[computerChoiceIndex];
    setComputerChoice(computerChoice || null);
    setLoading(false);
  };

  const handleLogin = async () => {
    if (username.trim()) {
      try {
        await createPlayer(username);
        setIsUserLoggedIn(true);
        setUser(username);
        resetGameState();
      } catch (error) {
        const axiosError = error as AxiosError;
        if (axiosError.response && axiosError.response.status === 400) {
            setErrorMessage("Player already exists, try with a new name.");
        } else {
            setErrorMessage("An error occurred. Please try again.");
        }
      }
    }
  };

  const handleLogout = () => {
    setUsername('');
    setIsUserLoggedIn(false);
    resetGameState();
    clearUser();
  };

  const resetGameState = () => {
    setUserChoice(null);
    setComputerChoice(null);
    setGameResult(null);
    setLoading(false);
    setErrorMessage("");
};


  return (
    <ThemeProvider theme={theme}>
      <Container
        style={{
          height: "100vh",
          width: "100%",
          display: "flex",
          flexDirection: "column",
          justifyContent: "space-between",
          alignItems: "center",
          padding: "20px",
        }}
      >
        <Header onLogout={handleLogout} isUserLoggedIn={isUserLoggedIn} />
        {!isUserLoggedIn ? (
          <Box
            style={{
              display: "flex",
              flexDirection: "column",
              alignItems: "center",
              justifyContent: "center",
              height: "100%",
            }}
          >
            <TextField
              placeholder="Enter your username"
              variant="outlined"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              error={!!errorMessage}
              helperText={errorMessage}
            />
            <Button variant="contained" color="primary" onClick={handleLogin}>
              Start game
            </Button>
          </Box>
        ) : (
          <>
            <Box style={{ display: "flex", justifyContent: "center", width: "100%" }}>
              <UserChoice userChoice={userChoice} username={username} />
              <ComputerChoice computerChoice={computerChoice} loading={loading} />
            </Box>
            <GameResult result={gameResult?.results || null} />
            <ChoiceSelector choices={choices} onChoiceSelect={handleUserChoice} />
          </>
        )}
      </Container>
    </ThemeProvider>
  );
}

export default App;
