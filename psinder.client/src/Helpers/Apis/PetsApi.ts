import { Pet } from "../Interfaces/PetInterface";

export const api = {
  GetAllPets: async (): Promise<Pet[]> => {
    const response = await fetch("/api/Pets/GetAllPets", {
      method: "GET",
    });

    if (!response.ok) {
      throw new Error("No pets fetched");
    }

    return response.json();
  },
  GetPetById: async (id: number): Promise<any> => {
    try {
      const response = await fetch(`/api/Pets/GetPet/${id}`, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
      });

      if (!response.ok) {
        throw new Error(`Failed to fetch pet with id ${id}`);
      }

      return response.json();
    } catch (error) {
      console.error(error);
      throw error;
    }
  },
  SearchPetByName: async (
    name: string
  ): Promise<{ isAuthenticated: boolean }> => {
    const response = await fetch(`/api/Pets/SearchPetByName/${name}`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    });

    if (!response.ok) {
      throw new Error("Authentication failed");
    }

    return response.json();
  },

  PutPet: async (pet: Pet): Promise<void> => {
    const response = await fetch("/api/Pets/PutPet", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(pet),
      credentials: "include",
    });

    if (!response.ok) {
      const error = await response.json();
      throw new Error(error.message || "Error while adding a pet");
    }
  },
  PostPet: async (pet: Pet): Promise<void> => {
    const response = await fetch("/api/Pets/PostPet", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(pet),
      credentials: "include",
    });

    if (!response.ok) {
      const error = await response.json();
      throw new Error(
        error.message || "Error while editing pet's informations"
      );
    }
  },
  DeletePet: async (id: number): Promise<any> => {
    const response = await fetch(`/api/Pets/DeletePet/${id}`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    });

    if (!response.ok) {
      throw new Error("Pet not found");
    }
    return response.status;
  },
};
