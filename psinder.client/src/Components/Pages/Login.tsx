import { z } from "zod";
import { Link, useNavigate } from "react-router-dom";
import { useAuth } from "@/Helpers/Auth";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "../ui/form";
import { Button } from "../ui/button";
import { Input } from "../ui/input";
import { useEffect } from "react";

const formSchema = z.object({
  email: z.string().min(3).max(50).email(),
  password: z.string().min(4).max(50),
});

const Login = () => {
  const navigate = useNavigate();
  const { isAuthenticated, login, errorMessage } = useAuth();
  useEffect(() => {
    if (isAuthenticated) {
      navigate("/");
    }
  }, [isAuthenticated, navigate]);

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      email: "",
      password: "",
    },
  });

  const onSubmit = async (values: z.infer<typeof formSchema>) => {
    await login(values.email, values.password);
  };
  return (
    <div className="page-wrapper p-8 mt-24 flex justify-center">
      <div className="p-8 rounded-md border w-96">
        <div className="text-red-700">{errorMessage}</div>
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)}>
            <FormField
              control={form.control}
              name="email"
              render={({ field }) => (
                <FormItem>
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
                      type="password"
                      placeholder="Password"
                      {...field}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <div className="mt-6 ">
              Don't have an account?{" "}
              <Link
                to="/register"
                className="text-blue-400 underline hover:no-underline"
              >
                Sign up
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

export default Login;
