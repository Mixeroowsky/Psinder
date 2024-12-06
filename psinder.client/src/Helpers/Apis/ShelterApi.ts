import { Shelter } from "../Interfaces/ShelterInterface";

export const api = {
  GetAllShelters: async (): Promise<Shelter[]> => {
    const response = await fetch("/api/Shelters/GetAllShelters", {
      method: "GET",
    });

    if (!response.ok) {
      throw new Error("No shelters fetched");
    }

    return response.json();
  },
  GetShelterById: async (id: number): Promise<Shelter> => {
    try {
      const response = await fetch(`/api/Shelters/GetShelterById/${id}`, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
      });

      if (!response.ok) {
        throw new Error(`Failed to fetch shelter with id ${id}`);
      }

      return response.json();
    } catch (error) {
      console.error(error);
      throw error;
    }
  },

  PutShelter: async (shelter: Shelter): Promise<void> => {
    const response = await fetch("/api/Shelters/UpdateShelter", {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(shelter),
      credentials: "include",
    });

    if (!response.ok) {
      const error = await response.json();
      throw new Error(
        error.message || "Error while editing shelter's informations"
      );
    }
  },
  PostShelter: async (shelter: Shelter): Promise<void> => {
    const response = await fetch("/api/Shelters/AddShelter", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(shelter),
      credentials: "include",
    });

    if (!response.ok) {
      const error = await response.json();
      throw new Error(error.message || "Error while adding a shelter");
    }
  },
  DeleteShelter: async (id: number): Promise<any> => {
    const response = await fetch(`/api/Shelters/DeleteShelter/${id}`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    });

    if (!response.ok) {
      throw new Error("Shelter not found");
    }
    return response.status;
  },
  CheckUser: async (id: number): Promise<number> => {
    try {
      const response = await fetch(`/api/Shelters/ShelterByUserId/${id}`, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
      });

      if (!response.ok) {
        throw new Error(`Failed to fetch shelter with id ${id}`);
      }
      return response.json();
    } catch (error) {
      console.error(error);
      throw error;
    }
  },
};
