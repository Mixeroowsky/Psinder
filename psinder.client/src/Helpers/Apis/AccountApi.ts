export const api = {
  login: async (email: string, password: string): Promise<void> => {
    const response = await fetch("/api/Account/login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ email, password }),
      credentials: "include",
    });

    if (!response.ok) {
      const error = await response.json();
      throw new Error(error.message || "Login failed");
    }
  },

  register: async (
    username: string,
    email: string,
    password: string
  ): Promise<void> => {
    const response = await fetch("/api/Account/register", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ username, email, password }),
      credentials: "include",
    });

    if (!response.ok) {
      const error = await response.json();
      throw new Error(error.message || "Registration failed");
    }
  },

  auth: async (): Promise<{ isAuthenticated: boolean }> => {
    const response = await fetch("/api/Account/auth", {
      method: "GET",
      credentials: "include",
    });

    if (!response.ok) {
      throw new Error("Authentication failed");
    }

    return response.json();
  },

  logout: async (): Promise<void> => {
    await fetch("/api/Account/logout", {
      method: "POST",
      credentials: "include",
    });
  },
};
