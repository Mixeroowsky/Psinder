import {
  createContext,
  ReactNode,
  useContext,
  useEffect,
  useState,
} from "react";

interface AuthProviderProps {
  children: ReactNode;
}
interface AuthContextType {
  isAuthenticated: boolean;
  login: (username: string, password: string) => Promise<void>;
  logout: () => Promise<void>;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);
export const AuthProvider = ({ children }: AuthProviderProps) => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);

  const login = async (email: string, password: string): Promise<void> => {
    try {
      const response = await fetch("/api/Account/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          email,
          password,
        }),
        credentials: "include",
      });
      if (response?.status == 200) {
        setIsAuthenticated(true);
      }
    } catch {}
  };

  const logout = async () => {
    try {
      await fetch("/api/Account/logout", {
        method: "POST",
        credentials: "include",
      });
      setIsAuthenticated(false);
    } catch {}
  };

  useEffect(() => {
    const checkAuth = async () => {
      const response = await fetch("/api/Account/auth", {
        method: "POST",
        credentials: "include",
      });
      const data = await response.json();
      console.log(data);
    };
    checkAuth();
  }, []);

  return (
    <AuthContext.Provider value={{ isAuthenticated, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};
export const useAuth = (): AuthContextType => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
};

const Auth = () => {
  return <div>Auth</div>;
};

export default Auth;
