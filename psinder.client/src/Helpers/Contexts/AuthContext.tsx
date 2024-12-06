import {
  createContext,
  ReactNode,
  useContext,
  useEffect,
  useState,
} from "react";
import { Outlet } from "react-router-dom";
import { api } from "../Apis/AccountApi";

interface AuthProviderProps {
  children: ReactNode;
}
interface AuthContextType {
  isAuthenticated: boolean;
  login: (username: string, password: string) => Promise<void>;
  logout: () => Promise<void>;
  errorMessage: string | null;
  userId: number | null;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: AuthProviderProps) => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);
  const [userId, setUserId] = useState<number | null>(null);

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
        const response = await api.auth();
        console.log("dupa?");
        setIsAuthenticated(true);
        setUserId(parseInt(response.userId));
      } catch {
        setIsAuthenticated(false);
      }
    };
    checkAuth();
  }, []);

  return (
    <AuthContext.Provider
      value={{ isAuthenticated, login, logout, errorMessage, userId }}
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