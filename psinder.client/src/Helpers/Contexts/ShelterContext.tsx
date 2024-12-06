import {
  createContext,
  ReactNode,
  useContext,
  useEffect,
  useState,
} from "react";
import { api as shelterApi } from "../Apis/ShelterApi";
import { api as accountApi } from "../Apis/AccountApi";
import { useNavigate } from "react-router-dom";

interface ProviderProps {
  children: ReactNode;
}
interface ShelterContextType {
  shelterId: number | null;
}

export const ShelterContext = createContext<ShelterContextType | undefined>(
  undefined
);

export const ShelterProvider = ({ children }: ProviderProps) => {
  const [shelterId, setShelterId] = useState<number | null>(null);

  const navigate = useNavigate();
  useEffect(() => {
    const checkShelter = async () => {
      try {
        const userId = parseInt((await accountApi.auth()).userId);
        const response = await shelterApi.CheckUser(userId);
        setShelterId(response);
      } catch {
        setShelterId(null);
      }
    };
    checkShelter();
  }, [navigate]);

  return (
    <ShelterContext.Provider value={{ shelterId }}>
      {children}
    </ShelterContext.Provider>
  );
};

export const shelterIdContext = () => {
  const context = useContext(ShelterContext);
  if (!context) {
    throw new Error("shelterId must be used within a ShelterProvider");
  }
  return context;
};
