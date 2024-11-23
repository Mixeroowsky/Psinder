import {
  createContext,
  ReactNode,
  useContext,
  useEffect,
  useState,
} from "react";
import { Outlet } from "react-router-dom";
import { api } from "./Apis/AccountApi";

interface AuthProviderProps {
  children: ReactNode;
}
interface AuthContextType {
  isAuthenticated: boolean;
  login: (username: string, password: string) => Promise<void>;
  logout: () => Promise<void>;
  errorMessage: string | null;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: AuthProviderProps) => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  const login = async (email: string, password: string): Promise<void> => {
    try {
      setErrorMessage(null);
      await api.login(email, password);
      setIsAuthenticated(true);
    } catch (error: any) {
      setErrorMessage("Invalid email or password.");
    }
  };

  const logout = async () => {
    try {
      await api.logout();
      setIsAuthenticated(false);
    } catch {}
  };

  useEffect(() => {
    const checkAuth = async () => {
      try {
        await api.auth();
        setIsAuthenticated(true);
      } catch {
        setIsAuthenticated(false);
      }
    };
    checkAuth();
  }, []);

  return (
    <AuthContext.Provider
      value={{ isAuthenticated, login, logout, errorMessage }}
    >
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
  useAuth();
  return <Outlet />;
};

export default Auth;
