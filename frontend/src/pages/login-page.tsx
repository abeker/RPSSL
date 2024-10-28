import { Box, TextField, Button } from "@mui/material";
import { useState } from "react";
import { createPlayer, getPlayerByName } from "../services/player-service";
import { AxiosError } from "axios";

interface LoginPageProps {
  onLogin: (username: string) => void;
}

function LoginPage({ onLogin }: LoginPageProps) {
  const [username, setUsername] = useState<string>("");
  const [errorMessage, setErrorMessage] = useState<string>("");
  const [showCreateButton, setShowCreateButton] = useState<boolean>(false);

  const handleLogin = async () => {
    if (username.trim()) {
      try {
        await getPlayerByName(username);
        onLogin(username);
      } catch (error) {
        const axiosError = error as AxiosError;
        if (axiosError.response && axiosError.response.status === 404) {
          setErrorMessage("Player does not exist. Please create a new player.");
          setShowCreateButton(true);
          setUsername('');
        } else {
          setErrorMessage("An error occurred. Please try again.");
        }
      }
    } else {
      setErrorMessage("Username cannot be empty.");
    }
  };

  const handleCreatePlayer = async () => {
    if (username.trim()) {
      try {
        await createPlayer(username);
        onLogin(username);
      } catch (error) {
        const axiosError = error as AxiosError;
        if (axiosError.response && axiosError.response.status === 400) {
            setErrorMessage("Player already exists, try with a new name.");
        } else {
            setErrorMessage("An error occurred. Please try again.");
        }
      }
    } else {
      setErrorMessage("Username cannot be empty.");
    }
  };

  return (
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
        onChange={(e) => {
          setUsername(e.target.value);
        }}
        error={!!errorMessage}
        helperText={errorMessage}
      />
      {!showCreateButton ? (
        <Button variant="contained" color="primary" onClick={handleLogin}>
          Play game
        </Button>
      ) : (
        <Button variant="contained" color="secondary" onClick={handleCreatePlayer}>
          Create player
        </Button>
      )}
    </Box>
  );
}

export default LoginPage;
