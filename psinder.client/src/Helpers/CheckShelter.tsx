import { createContext, ReactNode, useEffect, useState } from "react";
import { api as shelterApi } from "./Apis/ShelterApi";
import { api as accountApi } from "./Apis/AccountApi";

interface ProviderProps {
  children: ReactNode;
}
interface ShelterContextType {
  shelterRegistered: boolean;
}

export const ShelterContext = createContext<ShelterContextType>({
  shelterRegistered: false,
});

const CheckShelterProvider = ({ children }: ProviderProps) => {
  const [shelterRegistered, setShelterRegistered] = useState(false);

  useEffect(() => {
    const checkShelter = async () => {
      try {
        const userId = parseInt((await accountApi.auth()).userId);
        const response = await shelterApi.CheckUser(userId);
        if (response != null) {
          setShelterRegistered(true);
        } else {
          setShelterRegistered(false);
        }
      } catch {}
    };
    checkShelter();
  }, []);

  return (
    <ShelterContext.Provider value={{ shelterRegistered }}>
      {children}
    </ShelterContext.Provider>
  );
};

export default CheckShelterProvider;
