import { Link, useNavigate } from "react-router-dom";
import { useState, type FormEvent } from "react";
import {Eye, EyeOff} from "lucide-react";

type FormState = {
    email: string;
    password: string;
};

type Errors = {
    email?: string;
    password?: string;
    general?: string;
};

export function Login() {
    const navigate = useNavigate();

    const [form, setForm] = useState<FormState>({ email: "", password: "" });
    const [errors, setErrors] = useState<Errors>({});
    const [isLoading, setIsLoading] = useState(false);
    const [seePass, setSeePass] = useState(false);

    async function handleSubmit(e: FormEvent<HTMLFormElement>) {
        e.preventDefault();

        const newErrors: Errors = {};
        if (!form.email) newErrors.email = "Email is required";
        if (!form.password) newErrors.password = "Password is required";

        if (Object.keys(newErrors).length > 0) {
            setErrors(newErrors);
            return;
        }

        setErrors({});
        try {
            setIsLoading(true);
            // const uid = await login(form.email, form.password);
            navigate(`/`);
        } catch (err: unknown) {
            if (err instanceof Error) {
                const msg = err.message.toLowerCase();
                if (msg.includes("email")) setErrors({ email: err.message });
                else if (msg.includes("password")) setErrors({ password: err.message });
                else setErrors({ general: err.message });
            } else {
                setErrors({ general: "An unknown error occurred" });
            }
        } finally {
            setIsLoading(false);
        }
    }

    return (
        <div className="min-h-screen flex items-center justify-center px-4 bg-gray-950">
            <div className="w-full max-w-sm bg-gray-900 rounded-2xl shadow-xl p-6">
                <h2 className="text-2xl font-bold text-white text-center">Login</h2>
                <p className="text-gray-400 text-center mb-6 text-sm">Access your account</p>

                <form onSubmit={handleSubmit} className="space-y-4">
                    <div>
                        <input
                            type="email"
                            placeholder="Email"
                            value={form.email}
                            onChange={(e) => setForm({ ...form, email: e.target.value })}
                            className="w-full px-4 py-2 rounded-lg bg-gray-800 border border-gray-700 text-white placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-indigo-500"
                        />
                        {errors.email && <p className="text-red-400 text-sm mt-1">{errors.email}</p>}
                    </div>

                    <div className="relative w-full">
                        <input
                            type={seePass ? "text" : "password"}
                            placeholder="Password"
                            value={form.password}
                            onChange={(e) => setForm({ ...form, password: e.target.value })}
                            className="w-full px-4 py-2 rounded-lg bg-gray-800 border border-gray-700 text-white placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-indigo-500 pr-10"
                        />
                        <button
                            type="button"
                            onClick={() => setSeePass(!seePass)}
                            className="absolute inset-y-0 right-3 flex items-center top-1/2 text-gray-500"
                        >
                            {seePass ? <EyeOff size={20} /> : <Eye size={20} />}
                        </button>
                        {errors.password && <p className="text-red-500 text-sm mt-1">{errors.password}</p>}
                    </div>

                    {errors.general && <p className="text-red-500 text-center text-sm">{errors.general}</p>}

                    <button
                        type="submit"
                        disabled={isLoading}
                        className="w-full py-2 bg-gradient-to-r from-indigo-600 to-purple-600 text-white font-semibold rounded-xl transition-all duration-200 hover:from-indigo-700 hover:to-purple-700 focus:outline-none focus:ring-4 focus:ring-indigo-200 disabled:opacity-50 disabled:cursor-not-allowed transform hover:scale-[1.02] active:scale-[0.98]"
                    >
                        {isLoading ? (
                            <div className="flex items-center justify-center gap-3">
                                <div className="w-5 h-5 border-2 border-white/30 border-t-white rounded-full animate-spin" />
                                Logging to your account...
                            </div>
                        ) : (
                            "Login"
                        )}
                    </button>
                </form>

                <div className="mt-6 space-y-3">
                    <button className="w-full py-2 flex items-center justify-center gap-2 bg-white text-gray-800 font-semibold rounded-lg shadow hover:bg-gray-100 transition">
                        <img
                            src="https://www.svgrepo.com/show/475656/google-color.svg"
                            alt="Google"
                            className="w-5 h-5"
                        />
                        Continue with Google
                    </button>

                    <button className="w-full py-2 flex items-center justify-center gap-2 bg-gray-800 text-white font-semibold rounded-lg shadow hover:bg-gray-700 transition">
                        <img
                            src="https://www.svgrepo.com/show/512317/github-142.svg"
                            alt="GitHub"
                            className="w-5 h-5"
                        />
                        Continue with GitHub
                    </button>
                </div>

                <p className="text-gray-400 text-center mt-6 text-sm">
                    Don’t have an account?{" "}
                    <Link to="/register" className="text-indigo-400 hover:text-indigo-300 font-semibold">
                        Register
                    </Link>
                </p>
            </div>
        </div>
    );
}
