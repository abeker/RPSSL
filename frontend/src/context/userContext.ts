const USERNAME_KEY = "username";
const LOGIN_STATE_KEY = "isUserLoggedIn";

export const getStoredUser = () => {
  const storedUsername = localStorage.getItem(USERNAME_KEY);
  const storedLoginState = localStorage.getItem(LOGIN_STATE_KEY);

  return {
    username: storedUsername || "",
    isUserLoggedIn: storedLoginState === "true",
  };
};

export const setUser = (username: string) => {
  localStorage.setItem(USERNAME_KEY, username);
  localStorage.setItem(LOGIN_STATE_KEY, "true");
};

export const clearUser = () => {
  localStorage.removeItem(USERNAME_KEY);
  localStorage.removeItem(LOGIN_STATE_KEY);
};
