import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { Button } from "../../ui/button";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "../../ui/form";
import { Input } from "../../ui/input";
import { api } from "../../../Helpers/Apis/ShelterApi";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Shelter } from "@/Helpers/Interfaces/ShelterInterface";
import { useAuth } from "@/Helpers/Contexts/AuthContext";
import Spinner from "@/Helpers/Spinner";

const formSchema = z.object({
  name: z.string().min(2).max(50),
  city: z.string().min(2).max(2500),
  postCode: z
    .string()
    .regex(/^\d{2}-\d{3}$/, "Postal code must be in the format XX-XXX"),
  street: z.string().max(99),
  buildingNumber: z
    .string()
    .transform((val) => parseFloat(val))
    .refine((val) => !isNaN(val), {
      message: "Building number must be a number.",
    }),
  apartmentNumber: z
    .string()
    .max(9)
    .transform((val) => parseFloat(val))
    .refine((val) => !isNaN(val), {
      message: "Apartment number must be a number.",
    }),
  email: z.string().email(),
  phoneNumber: z.string().max(9, "Enter a valid phone number"),
});

const AddEditShelter = () => {
  const { id } = useParams();
  const [message, setMessage] = useState("");
  const [isLoading, setIsLoading] = useState(false);
  const [forEdit, setForEdit] = useState(false);
  const navigate = useNavigate();

  const { userId } = useAuth();

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
  });
  const { reset } = form;
  useEffect(() => {
    const fetchShelter = async () => {
      if (!id) return;

      try {
        setIsLoading(true);
        const response = await fetch(`/api/Shelters/GetShelterById/${id}`);
        if (response.ok) {
          setForEdit(true);
          const data = await response.json();
          reset({
            name: data.name,
            city: data.city,
            postCode: data.postCode,
            street: data.street,
            buildingNumber: data.buildingNumber.toString(),
            apartmentNumber: data.apartmentNumber.toString(),
            email: data.email,
            phoneNumber: data.phoneNumber,
          });
        } else {
          console.error("Failed to fetch shelter data");
        }
      } catch (error) {
        console.error("Error:", error);
        setForEdit(false);
      } finally {
        setIsLoading(false);
      }
    };

    fetchShelter();
  }, [id]);

  const onSubmit = async (values: z.infer<typeof formSchema>) => {
    console.log(userId);
    if (userId != null) {
      const shelter: Shelter = {
        name: values.name,
        city: values.city,
        postCode: values.postCode,
        street: values.street,
        buildingNumber: values.buildingNumber,
        apartmentNumber: values.apartmentNumber,
        phoneNumber: values.phoneNumber,
        email: values.email,
        userId: userId,
      };
      if (forEdit) {
        try {
          if (id != null) {
            await api.PutShelter(parseInt(id), shelter);
            navigate("/");
          }
        } catch (err: any) {
          setMessage(err.message);
        }
      } else {
        try {
          await api.PostShelter(shelter);
          setMessage("Shelter successfully registered");
          navigate("/");
        } catch (err: any) {
          setMessage("Shelter by this name already exists");
        }
      }
    } else {
      alert("Error while adding a shelter - user not logged in");
    }
  };
  const deleteShelter = async () => {
    if (
      confirm("Are you sure you want to delete your shelter?") &&
      id != null
    ) {
      try {
        await api.DeleteShelter(parseInt(id));
      } catch {}
      navigate("/");
    }
  };
  return (
    <div className="flex p-10 justify-center ">
      {isLoading ? (
        <div className="text-center">
          <Spinner />
        </div>
      ) : (
        <Form {...form}>
          <form
            onSubmit={form.handleSubmit(onSubmit)}
            className="space-y-3 w-1/2"
          >
            <h1 className="danger text-red-500">{message}</h1>
            {forEdit ? (
              <h1 className="mb-10">Edit your shelter</h1>
            ) : (
              <h1 className="mb-10">Adding new shelter</h1>
            )}
            <FormField
              control={form.control}
              name="name"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Shelter name</FormLabel>
                  <FormControl>
                    <Input
                      placeholder="Shelter name"
                      className=" h-12 max-w-sm"
                      {...field}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="city"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>City</FormLabel>
                  <FormControl>
                    <Input
                      placeholder="City"
                      className="h-12 max-w-sm"
                      {...field}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="postCode"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Post code</FormLabel>
                  <FormControl>
                    <Input
                      placeholder="Post code"
                      {...field}
                      maxLength={6}
                      className="h-12 max-w-24"
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="street"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Street</FormLabel>
                  <FormControl>
                    <Input
                      placeholder="Street"
                      className="h-12 max-w-sm"
                      {...field}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="buildingNumber"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Building number</FormLabel>
                  <FormControl>
                    <Input
                      type="number"
                      placeholder="Building number"
                      className="h-12 max-w-sm [&::-webkit-inner-spin-button]:appearance-none"
                      {...field}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="apartmentNumber"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Apartment number</FormLabel>
                  <FormControl>
                    <Input
                      type="number"
                      placeholder="Apartment number"
                      className=" h-12 max-w-sm [&::-webkit-inner-spin-button]:appearance-none"
                      {...field}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="phoneNumber"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Phone number</FormLabel>
                  <FormControl>
                    <Input
                      type="number"
                      placeholder="Phone number"
                      className=" h-12 max-w-sm [&::-webkit-inner-spin-button]:appearance-none"
                      {...field}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="email"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Email</FormLabel>
                  <FormControl>
                    <Input
                      placeholder="Email"
                      className=" h-12 max-w-sm"
                      {...field}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <div className="flex flex-row justify-between ">
              <Button className="mt-8 w-24" type="submit">
                Submit
              </Button>
              {forEdit && (
                <Button
                  variant="destructive"
                  className="mt-8"
                  type="button"
                  onClick={deleteShelter}
                >
                  Delete your shelter
                </Button>
              )}
            </div>
          </form>
        </Form>
      )}
    </div>
  );
};

export default AddEditShelter;
