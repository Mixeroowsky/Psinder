import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { Button } from "../ui/button";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "../ui/form";
import { Input } from "../ui/input";
import { api } from "../../Helpers/Apis/AccountApi";
import { Link, useNavigate } from "react-router-dom";
import { useState } from "react";
import { useAuth } from "../../Helpers/Contexts/AuthContext";

const formSchema = z
  .object({
    username: z.string().min(3).max(50),
    email: z.string().min(3).max(50).email(),
    password: z.string().min(4).max(50),
    confirmPassword: z.string().min(4).max(50),
  })
  .refine(
    (data) => {
      return data.password === data.confirmPassword;
    },
    { message: "Passwords do not match", path: ["confirmPassword"] }
  );

const Register = () => {
  const [errorMessage, setError] = useState("");
  const navigate = useNavigate();
  const { login } = useAuth();
  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: { email: "" },
  });
  const onSubmit = async (values: z.infer<typeof formSchema>) => {
    try {
      await api.register(values.username, values.email, values.password);
    } catch (err: any) {
      setError("User already exists");
      return;
    }
    await login(values.email, values.password);
    navigate("/");
  };
  return (
    <div className="page-wrapper p-8 mt-24 flex justify-center">
      <div className="p-8 rounded-md border w-96">
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)}>
            <p className="text-red-500">{errorMessage}</p>
            <FormField
              control={form.control}
              name="username"
              render={({ field }) => (
                <FormItem className="mt-4">
                  <FormLabel>Username</FormLabel>
                  <FormControl>
                    <Input
                      className="h-12 p-4 text-lg"
                      placeholder="Username"
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
                <FormItem className="mt-4">
                  <FormLabel>Email address</FormLabel>
                  <FormControl>
                    <Input
                      className="h-12 p-4 text-lg"
                      placeholder="Email address"
                      type="email"
                      {...field}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="password"
              render={({ field }) => (
                <FormItem className="mt-4">
                  <FormLabel>Password</FormLabel>
                  <FormControl>
                    <Input
                      className="h-12 p-4 text-lg"
                      placeholder="Password"
                      type="password"
                      {...field}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="confirmPassword"
              render={({ field }) => (
                <FormItem className="mt-4">
                  <FormLabel>Confirm password</FormLabel>
                  <FormControl>
                    <Input
                      className="h-12 p-4 text-lg"
                      placeholder="Password"
                      type="password"
                      {...field}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <div className="mt-6 ">
              Already have an account?{" "}
              <Link
                to="/login"
                className="text-blue-400 underline hover:no-underline"
              >
                Log in
              </Link>
            </div>
            <Button className="mt-6 h-12 w-full" type="submit">
              Submit
            </Button>
          </form>
        </Form>
      </div>
    </div>
  );
};

export default Register;
