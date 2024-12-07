import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "../../ui/select";
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
import { Textarea } from "@/Components/ui/textarea";
import FileUpload from "@/Helpers/FileUpload";
import { api } from "../../../Helpers/Apis/PetsApi";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

const formSchema = z.object({
  name: z.string().min(2).max(50),
  description: z.string().min(2).max(2500),
  sex: z.string(),
  age: z.string().max(99),
  breedType: z.string(),
  image: z.instanceof(File).refine((file) => file.type.startsWith("image/"), {
    message: "Please upload a valid image file",
  }),
});

const AddPet = () => {
  const [message, setMessage] = useState("");
  const navigate = useNavigate();
  const formData = new FormData();

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      name: "",
      description: "",
      sex: "0",
      age: "0",
      breedType: "0",
    },
  });

  const onSubmit = async (values: z.infer<typeof formSchema>) => {
    if (values.image.size > 0) {
      if (values.image.size < 5120 * 5120)
        setMessage("Image size should be less than 5 MB");
    }
    formData.append("name", values.name);
    formData.append("description", values.description);
    formData.append("sex", values.sex.toString());
    formData.append("age", values.age.toString());
    formData.append("breedType", values.breedType.toString());
    formData.append("shelterId", "1");
    formData.append("imageFile", values.image);

    try {
      await api.PostPet(formData);
      setMessage("Pet successfully added");
      navigate("/pets");
    } catch (err: any) {
      setMessage(err.message);
    }
  };

  return (
    <div className="">
      <div className="m-10 text-xl">{message}</div>
      <Form {...form}>
        <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
          <div className="flex">
            <div className="flex-1 m-10">
              <FormField
                control={form.control}
                name="name"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Name</FormLabel>
                    <FormControl>
                      <Input
                        className="text-xl h-16 max-w-sm"
                        placeholder="Billy"
                        {...field}
                      />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="description"
                render={({ field }) => (
                  <FormItem className="mt-4">
                    <FormLabel>Description</FormLabel>
                    <FormControl>
                      <Textarea
                        className="text-md max-w-3xl h-32"
                        placeholder="Type your description here."
                        {...field}
                      />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="sex"
                render={({ field }) => (
                  <FormItem className="mt-4">
                    <FormLabel>Sex</FormLabel>
                    <Select
                      onValueChange={field.onChange}
                      defaultValue={String(field.value)}
                    >
                      <FormControl>
                        <SelectTrigger className="max-w-sm w-32">
                          <SelectValue placeholder="Male" />
                        </SelectTrigger>
                      </FormControl>
                      <SelectContent>
                        <SelectItem value="0">Male</SelectItem>
                        <SelectItem value="1">Female</SelectItem>
                      </SelectContent>
                    </Select>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="age"
                render={({ field }) => (
                  <FormItem className="mt-4">
                    <FormLabel>Age</FormLabel>
                    <FormControl>
                      <Input
                        min="0"
                        max="99"
                        maxLength={2}
                        type="number"
                        className="max-w-sm w-14 [&::-webkit-inner-spin-button]:appearance-none"
                        placeholder="0"
                        {...field}
                      />
                    </FormControl>

                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="breedType"
                render={({ field }) => (
                  <FormItem className="mt-4">
                    <FormLabel>Breed type</FormLabel>
                    <Select
                      onValueChange={field.onChange}
                      defaultValue={field.value}
                    >
                      <FormControl>
                        <SelectTrigger className="max-w-sm w-32">
                          <SelectValue placeholder="Select a verified email to display" />
                        </SelectTrigger>
                      </FormControl>
                      <SelectContent>
                        <SelectItem value="0">Dog</SelectItem>
                        <SelectItem value="1">Cat</SelectItem>
                        <SelectItem value="2">Other</SelectItem>
                      </SelectContent>
                    </Select>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <Button className="mt-8 w-24" type="submit">
                Submit
              </Button>
            </div>
            <div className="flex-1 mt-16 p-10 ">
              <FormField
                control={form.control}
                name="image"
                render={({ field }) => (
                  <FormItem>
                    <FormControl>
                      <FileUpload field={field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
            </div>
          </div>
        </form>
      </Form>
    </div>
  );
};

export default AddPet;
