"use client"

import * as React from "react"
import { motion, useMotionValue, useTransform, useSpring, useInView, useReducedMotion } from "framer-motion"
import ScrollProgressComponent from "./ScrollProgress" // Import the ScrollProgress component
import MarqueeComponent from "./Marquee" // Import the Marquee component

export default function Page() {
    // Local luxury palette variables (kept inside the component to avoid editing globals)
    const luxVars: React.CSSProperties = {
        // Deep backdrop and accents tuned via OKLCH for premium contrast
        ["--lux-gold" as any]: "oklch(0.85 0.12 85)", // rich gold
        ["--lux-beige" as any]: "oklch(0.92 0.03 95)", // soft beige
        ["--lux-royal" as any]: "oklch(0.42 0.1 280)", // royal indigo
        ["--lux-ink" as any]: "oklch(0.13 0.02 265)", // near-black with indigo undertone
        ["--lux-glass" as any]: "oklch(1 0 0 / 0.06)",
        ["--lux-ring" as any]: "oklch(0.75 0.07 85 / 0.35)",
    }

    return (
        <main className="relative min-h-screen overflow-hidden bg-background text-foreground" style={luxVars}>
            <ScrollProgressComponent />
            <AuroraBackground />

            <header className="relative z-10">
                <Nav />
            </header>

            <Hero />
            {/* Add marquee of notable artists/collectors for a cinematic brand feel */}
            <MarqueeComponent />

            <section id="gallery" className="relative z-10">
                <FeaturedArtworks />
            </section>

            <section id="stats" className="relative z-10">
                <Stats />
            </section>

            <section id="cta" className="relative z-10">
                <Cta />
            </section>

            <footer className="relative z-10 border-t border-border/50">
                <div className="mx-auto max-w-7xl px-6 py-8 text-sm text-muted-foreground">
                    <p className="text-pretty">© {new Date().getFullYear()} Atelier Lux. All rights reserved.</p>
                </div>
            </footer>
        </main>
    )
}

/* --------------------------------- Nav ---------------------------------- */

function Nav() {
    const scrollTo = (id: string) => {
        const el = document.getElementById(id)
        if (el) el.scrollIntoView({ behavior: "smooth", block: "start" })
    }
    return (
        <nav className="mx-auto flex w-full max-w-7xl items-center justify-between px-6 py-5">
            <div className="flex items-center gap-2">
                <span aria-hidden className="h-2 w-2 rounded-full bg-[var(--lux-gold)] ring-2 ring-[var(--lux-ring)]" />
                <span className="text-sm tracking-widest text-muted-foreground">ATELIER</span>
                <span className="font-serif text-lg font-semibold tracking-tight">LUX</span>
            </div>
            <div className="hidden items-center gap-6 md:flex">
                <button
                    onClick={() => scrollTo("gallery")}
                    className="text-sm text-muted-foreground transition-colors hover:text-foreground"
                    aria-label="Explore Gallery"
                >
                    Gallery
                </button>
                <button
                    onClick={() => scrollTo("stats")}
                    className="text-sm text-muted-foreground transition-colors hover:text-foreground"
                    aria-label="View Achievements"
                >
                    Achievements
                </button>
                <button
                    onClick={() => scrollTo("cta")}
                    className="text-sm text-muted-foreground transition-colors hover:text-foreground"
                    aria-label="Meet the Artists"
                >
                    Meet Artists
                </button>
            </div>
        </nav>
    )
}

/* -------------------------------- Hero ----------------------------------- */

function Hero() {
    // Parallax motion for floating art, synced to mouse
    const containerRef = React.useRef<HTMLDivElement>(null)
    const mx = useMotionValue(0)
    const my = useMotionValue(0)
    const rx = useSpring(useTransform(my, [-50, 50], [8, -8]), { stiffness: 150, damping: 20 })
    const ry = useSpring(useTransform(mx, [-50, 50], [-8, 8]), { stiffness: 150, damping: 20 })

    const [spot, setSpot] = React.useState({ x: 0, y: 0 })
    const prefersReduced = useReducedMotion()

    const onMouseMove = (e: React.MouseEvent) => {
        const rect = containerRef.current?.getBoundingClientRect()
        if (!rect) return
        const x = e.clientX - (rect.left + rect.width / 2)
        const y = e.clientY - (rect.top + rect.height / 2)
        mx.set(Math.max(-50, Math.min(50, x / 10)))
        my.set(Math.max(-50, Math.min(50, y / 10)))
        setSpot({ x: e.clientX - rect.left, y: e.clientY - rect.top })
    }

    return (
        <section
            ref={containerRef}
            onMouseMove={onMouseMove}
            className="relative mx-auto flex min-h-[80vh] w-full max-w-7xl items-center justify-between px-6 pt-8 pb-24"
            aria-label="Hero"
        >
            <div className="pointer-events-none absolute inset-0">
                <motion.div
                    style={{
                        x: (spot.x || 0) - 200,
                        y: (spot.y || 0) - 200,
                        opacity: prefersReduced ? 0 : 0.18,
                    }}
                    transition={{ type: "spring", stiffness: 120, damping: 18 }}
                    className="absolute h-[400px] w-[400px] rounded-full"
                >
                    <div
                        className="h-full w-full rounded-full"
                        style={{
                            background: "radial-gradient(closest-side, oklch(1 0 0 / 0.12) 0%, transparent 70%)",
                            mixBlendMode: "overlay",
                        }}
                    />
                </motion.div>
            </div>
            <div className="relative z-10 max-w-2xl">
                <motion.p
                    initial={{ opacity: 0, y: 10 }}
                    animate={{ opacity: 0.7, y: 0 }}
                    transition={{ duration: 0.6, delay: 0.05, ease: "easeOut" }}
                    className="mb-4 text-sm tracking-widest text-muted-foreground"
                >
                    WORLD-CLASS GALLERY
                </motion.p>
                <motion.h1
                    initial={{ opacity: 0, y: 16 }}
                    animate={{ opacity: 1, y: 0 }}
                    transition={{ duration: 0.8, ease: "easeOut" }}
                    className="text-pretty font-serif text-4xl font-semibold leading-tight md:text-6xl"
                >
                    Where modern elegance meets timeless mastery.
                </motion.h1>
                <motion.p
                    initial={{ opacity: 0, y: 8 }}
                    animate={{ opacity: 1, y: 0 }}
                    transition={{ duration: 0.7, delay: 0.1, ease: "easeOut" }}
                    className="mt-5 max-w-xl text-balance text-base leading-relaxed text-muted-foreground md:text-lg"
                >
                    An immersive curation of contemporary art, presented with cinematic finesse, refined typography, and luxury
                    detail.
                </motion.p>

                <motion.div
                    initial="hidden"
                    animate="show"
                    variants={{
                        hidden: { opacity: 0, y: 10 },
                        show: { opacity: 1, y: 0, transition: { delayChildren: 0.15, staggerChildren: 0.08 } },
                    }}
                    className="mt-8 flex flex-wrap items-center gap-3"
                >
                    <Button
                        onClick={() => document.getElementById("gallery")?.scrollIntoView({ behavior: "smooth" })}
                        variant="gold"
                    >
                        Explore Gallery
                    </Button>
                    <Button
                        onClick={() => document.getElementById("cta")?.scrollIntoView({ behavior: "smooth" })}
                        variant="ghost"
                    >
                        Meet Artists
                    </Button>
                </motion.div>
            </div>

            {/* Parallax floating art */}
            <div className="pointer-events-none relative hidden h-[44vh] w-[44vh] md:block">
                <FloatingArt
                    style={{ rotateX: rx, rotateY: ry, x: ry, y: rx }}
                    img="/abstract-painting-on-canvas.jpg"
                    alt="Abstract canvas"
                    className="left-6 top-0 h-[60%] w-[60%]"
                    delay={0}
                />
                <FloatingArt
                    style={{ rotateX: rx, rotateY: ry, x: ry, y: rx }}
                    img="/sculpture-marble-bust.jpg"
                    alt="Marble sculpture"
                    className="right-0 top-24 h-[58%] w-[58%]"
                    delay={0.1}
                />
                <FloatingArt
                    style={{ rotateX: rx, rotateY: ry, x: ry, y: rx }}
                    img="/gallery-installation-lights.jpg"
                    alt="Gallery installation"
                    className="left-20 bottom-3 h-[42%] w-[64%]"
                    delay={0.2}
                />
            </div>
        </section>
    )
}

function FloatingArt({
                         img,
                         alt,
                         className,
                         style,
                         delay = 0,
                     }: {
    img: string
    alt: string
    className?: string
    style?: any
    delay?: number
}) {
    return (
        <motion.div
            initial={{ opacity: 0, y: 12, scale: 0.98 }}
            animate={{ opacity: 1, y: 0, scale: 1 }}
            transition={{ duration: 0.8, delay, ease: "easeOut" }}
            style={style}
            className={`absolute rounded-xl border border-white/10 bg-[var(--lux-glass)] p-1 backdrop-blur-md ${className}`}
        >
            <div className="relative h-full w-full overflow-hidden rounded-[calc(theme(--radius-lg))] ring-1 ring-[var(--lux-ring)]">
                <img
                    src={img || "/placeholder.svg"}
                    alt={alt}
                    className="h-full w-full object-cover transition-transform duration-500"
                />
                <div
                    aria-hidden="true"
                    className="pointer-events-none absolute inset-0 bg-[radial-gradient(120px_80px_at_30%_0%,rgba(255,255,255,0.18),transparent_60%)]"
                />
            </div>
        </motion.div>
    )
}

/* ------------------------- Featured Artworks Grid ------------------------ */

function FeaturedArtworks() {
    const containerRef = React.useRef<HTMLDivElement>(null)
    const inView = useInView(containerRef, { margin: "-100px", once: true })

    const items = [
        {
            title: "Nocturne in Gold",
            img: "/dark-gold-abstract-art.jpg",
        },
        {
            title: "Indigo Reverie",
            img: "/indigo-abstract-painting.jpg",
        },
        {
            title: "Marble Halcyon",
            img: "/minimal-marble-texture.jpg",
        },
        {
            title: "Gossamer Light",
            img: "/soft-light-bokeh-art.jpg",
        },
        {
            title: "Crown of Dust",
            img: "/earth-tone-textured-painting.jpg",
        },
        {
            title: "Silent Indigo",
            img: "/deep-blue-canvas-painting.jpg",
        },
    ]

    return (
        <div className="mx-auto max-w-7xl px-6 py-20">
            <motion.h2
                initial={{ opacity: 0, y: 8 }}
                whileInView={{ opacity: 1, y: 0 }}
                viewport={{ once: true, amount: 0.6 }}
                transition={{ duration: 0.7, ease: "easeOut" }}
                className="text-pretty font-serif text-3xl font-semibold md:text-4xl"
            >
                Featured Artworks
            </motion.h2>
            <motion.p
                initial={{ opacity: 0, y: 6 }}
                whileInView={{ opacity: 1, y: 0 }}
                viewport={{ once: true, amount: 0.6 }}
                transition={{ duration: 0.65, delay: 0.05, ease: "easeOut" }}
                className="mt-3 max-w-2xl text-pretty text-muted-foreground"
            >
                A refined curation, presented with glass-morphism, subtle reflections, and immersive motion.
            </motion.p>

            <div ref={containerRef} className="mt-10 grid grid-cols-1 gap-6 sm:grid-cols-2 lg:grid-cols-3">
                {items.map((item, i) => (
                    <ArtworkCard key={i} {...item} index={i} inView={inView} />
                ))}
            </div>
        </div>
    )
}

function ArtworkCard({
                         title,
                         img,
                         index,
                         inView,
                     }: {
    title: string
    img: string
    index: number
    inView: boolean
}) {
    return (
        <motion.article
            initial={{ opacity: 0, y: 16, scale: 0.98 }}
            animate={inView ? { opacity: 1, y: 0, scale: 1 } : {}}
            transition={{ duration: 0.7, delay: 0.06 * index, ease: "easeOut" }}
            whileHover={{ y: -6, rotateX: -1, rotateY: 1 }}
            className="group relative will-change-transform overflow-hidden rounded-xl border border-white/10 bg-[var(--lux-glass)] ring-1 ring-[var(--lux-ring)]"
        >
            <div className="relative aspect-[4/3] overflow-hidden">
                <img
                    src={img || "/placeholder.svg"}
                    alt={title}
                    className="h-full w-full object-cover transition-transform duration-700 group-hover:scale-[1.05]"
                />
                <div
                    aria-hidden="true"
                    className="pointer-events-none absolute inset-0 bg-[radial-gradient(120px_80px_at_30%_0%,rgba(255,255,255,0.18),transparent_60%)] opacity-0 transition-opacity duration-500 group-hover:opacity-100"
                />
            </div>
            <div className="flex items-center justify-between px-4 py-3">
                <h3 className="text-pretty font-serif text-lg">{title}</h3>
                <div className="flex items-center gap-1 text-xs text-muted-foreground">
                    <StarIcon className="size-4 text-[var(--lux-gold)]" />
                    <span>Curated</span>
                </div>
            </div>
        </motion.article>
    )
}

/* -------------------------------- Stats ---------------------------------- */

function Stats() {
    return (
        <div className="mx-auto max-w-7xl px-6 py-20">
            <motion.h2
                initial={{ opacity: 0, y: 8 }}
                whileInView={{ opacity: 1, y: 0 }}
                viewport={{ once: true, amount: 0.6 }}
                transition={{ duration: 0.7, ease: "easeOut" }}
                className="text-pretty font-serif text-3xl font-semibold md:text-4xl"
            >
                Achievements
            </motion.h2>
            <div className="mt-8 grid grid-cols-1 gap-6 sm:grid-cols-3">
                <StatCard label="Featured Artworks" value={320} />
                <StatCard label="Artists" value={86} />
                <StatCard label="Awards" value={24} />
            </div>
        </div>
    )
}

function StatCard({ label, value }: { label: string; value: number }) {
    const ref = React.useRef<HTMLDivElement>(null)
    const inView = useInView(ref, { once: true, margin: "-80px" })
    const count = useSpring(0, { stiffness: 55, damping: 14 })
    React.useEffect(() => {
        if (inView) count.set(value)
    }, [inView, count, value])
    const rounded = useTransform(count, (latest) => Math.round(latest))

    return (
        <motion.div
            ref={ref}
            initial={{ opacity: 0, y: 12 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true, amount: 0.6 }}
            transition={{ duration: 0.6, ease: "easeOut" }}
            className="relative overflow-hidden rounded-xl border border-white/10 bg-[var(--lux-glass)] p-6 ring-1 ring-[var(--lux-ring)]"
        >
            <div
                aria-hidden="true"
                className="pointer-events-none absolute -right-10 -top-10 size-28 rounded-full bg-[var(--lux-gold)]/15 blur-2xl"
            />
            <div className="flex items-center gap-3">
                <div className="flex size-10 items-center justify-center rounded-md bg-gradient-to-br from-[var(--lux-royal)] to-[var(--lux-gold)] ring-1 ring-[var(--lux-ring)]">
                    <StarIcon className="size-5 text-[var(--lux-beige)]" />
                </div>
                <div>
                    <motion.div className="font-serif text-3xl font-semibold">
                        {/* @ts-expect-error - motion value in children */}
                        {rounded}
                    </motion.div>
                    <p className="text-sm text-muted-foreground">{label}</p>
                </div>
            </div>
        </motion.div>
    )
}

/* --------------------------------- CTA ----------------------------------- */

function Cta() {
    return (
        <div className="mx-auto max-w-7xl px-6 pb-28 pt-8">
            <motion.div
                initial={{ opacity: 0, y: 12 }}
                whileInView={{ opacity: 1, y: 0 }}
                viewport={{ once: true, amount: 0.5 }}
                transition={{ duration: 0.7, ease: "easeOut" }}
                className="relative overflow-hidden rounded-2xl ring-1 ring-[var(--lux-ring)]"
            >
                <div className="absolute inset-0 bg-gradient-to-r from-[var(--lux-royal)]/85 to-[var(--lux-gold)]/70" />
                <div className="relative isolate px-8 py-14 md:px-12 md:py-16">
                    <motion.h3
                        initial={{ opacity: 0, y: 8 }}
                        whileInView={{ opacity: 1, y: 0 }}
                        viewport={{ once: true }}
                        transition={{ duration: 0.6, ease: "easeOut" }}
                        className="text-pretty font-serif text-3xl font-semibold text-[var(--lux-beige)] md:text-4xl"
                    >
                        Experience the private collection.
                    </motion.h3>
                    <motion.p
                        initial={{ opacity: 0, y: 6 }}
                        whileInView={{ opacity: 1, y: 0 }}
                        viewport={{ once: true }}
                        transition={{ duration: 0.6, delay: 0.05, ease: "easeOut" }}
                        className="mt-5 max-w-xl text-balance text-[var(--lux-beige)]/85"
                    >
                        Request a private viewing with our curators and discover rare works before they debut.
                    </motion.p>
                    <div className="mt-7 flex flex-wrap gap-3">
                        <Button variant="beige">Request Access</Button>
                        <Button variant="ghostLight">Contact Curator</Button>
                    </div>
                </div>
            </motion.div>
        </div>
    )
}

/* ----------------------------- UI Primitives ----------------------------- */

function Button({
                    children,
                    onClick,
                    variant = "gold",
                }: {
    children: React.ReactNode
    onClick?: () => void
    variant?: "gold" | "ghost" | "beige" | "ghostLight"
}) {
    const base =
        "relative inline-flex items-center justify-center rounded-md px-4 py-2 text-sm transition-all focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-[var(--lux-ring)] overflow-hidden group"
    const variants: Record<string, string> = {
        gold:
            "bg-gradient-to-b from-[var(--lux-gold)] to-[oklch(0.75_0.09_85)] text-[var(--lux-ink)] " +
            "shadow-[0_1px_0_rgba(0,0,0,0.2)_inset,0_8px_20px_-8px_rgba(255,215,0,0.35)] " +
            "hover:brightness-[1.03] active:scale-[0.99]",
        ghost:
            "border border-white/15 bg-[var(--lux-glass)] text-foreground backdrop-blur " +
            "hover:bg-white/10 hover:border-white/25",
        beige: "bg-[var(--lux-beige)] text-[var(--lux-ink)] hover:brightness-105 shadow-[0_8px_24px_-10px_rgba(0,0,0,0.3)]",
        ghostLight: "border border-[var(--lux-beige)]/30 text-[var(--lux-beige)] hover:bg-[var(--lux-beige)]/10",
    }
    return (
        <motion.button
            whileHover={{ y: -1 }}
            whileTap={{ scale: 0.98 }}
            onClick={onClick}
            className={`${base} ${variants[variant]}`}
        >
            <span className="relative z-10">{children}</span>
            <span
                aria-hidden="true"
                className="pointer-events-none absolute inset-0 rounded-md ring-1 ring-inset ring-black/5"
            />
            <motion.span
                aria-hidden="true"
                initial={{ x: "-120%" }}
                whileHover={{ x: "120%" }}
                transition={{ duration: 1.2, ease: "easeInOut" }}
                className="pointer-events-none absolute -inset-y-2 -left-1/3 z-0 h-[140%] w-1/3 rotate-12 bg-white/15 blur-md"
            />
        </motion.button>
    )
}

/* ------------------------- Background Animations ------------------------- */

function AuroraBackground() {
    // Floating aurora lights: elegant, subdued motion for cinematic depth
    const lights = [
        { size: 520, x: "10%", y: "6%", color: "var(--lux-royal)", delay: 0 },
        { size: 420, x: "70%", y: "18%", color: "var(--lux-gold)", delay: 0.2 },
        { size: 560, x: "40%", y: "70%", color: "oklch(0.62 0.07 260)", delay: 0.35 },
    ] as const

    const sparkles = [
        { x: "8%", y: "22%", d: 0 },
        { x: "18%", y: "58%", d: 0.2 },
        { x: "28%", y: "36%", d: 0.35 },
        { x: "46%", y: "14%", d: 0.5 },
        { x: "62%", y: "64%", d: 0.15 },
        { x: "74%", y: "30%", d: 0.4 },
        { x: "86%", y: "52%", d: 0.6 },
        { x: "34%", y: "78%", d: 0.25 },
    ] as const

    return (
        <div className="pointer-events-none absolute inset-0">
            <div className="absolute inset-0 bg-[radial-gradient(260px_180px_at_50%_-10%,rgba(255,255,255,0.05),transparent_70%)]" />
            {lights.map((l, i) => (
                <motion.div
                    key={i}
                    initial={{ opacity: 0, scale: 0.95 }}
                    animate={{
                        opacity: 0.35,
                        scale: 1,
                        y: [0, -20, 0],
                        x: [0, 10, 0],
                    }}
                    transition={{
                        delay: l.delay,
                        duration: 12,
                        repeat: Number.POSITIVE_INFINITY,
                        repeatType: "mirror",
                        ease: "easeInOut",
                    }}
                    style={{
                        left: l.x,
                        top: l.y,
                        width: l.size,
                        height: l.size,
                        background: `radial-gradient(closest-side, ${l.color} 0%, transparent 60%)`,
                        filter: "blur(40px)",
                    }}
                    className="absolute rounded-full mix-blend-screen"
                />
            ))}
            {sparkles.map((s, i) => (
                <motion.div
                    key={`sp-${i}`}
                    className="absolute rounded-full"
                    style={{ left: s.x, top: s.y, width: 3, height: 3, background: "oklch(0.95 0.02 90)" }}
                    initial={{ opacity: 0.2, y: 0 }}
                    animate={{ opacity: [0.15, 0.5, 0.15], y: [0, -6, 0] }}
                    transition={{ delay: s.d, duration: 3.6, repeat: Number.POSITIVE_INFINITY, ease: "easeInOut" }}
                />
            ))}
            {/* Subtle vignette for cinematic focus */}
            <div className="absolute inset-0 bg-[radial-gradient(1200px_600px_at_50%_0%,transparent,rgba(0,0,0,0.25))]" />
            <div className="absolute inset-0 bg-[linear-gradient(to_bottom,transparent,rgba(0,0,0,0.35))]" />
        </div>
    )
}

/* -------------------------------- Icons ---------------------------------- */

function StarIcon({ className = "size-4" }: { className?: string }) {
    return (
        <svg aria-hidden="true" viewBox="0 0 24 24" className={className} fill="currentColor" role="img">
            <path d="M12 3.6l2.1 4.26 4.7.68-3.4 3.31.8 4.67L12 14.96l-4.2 2.56.8-4.67-3.4-3.31 4.7-.68L12 3.6z" />
        </svg>
    )
}
