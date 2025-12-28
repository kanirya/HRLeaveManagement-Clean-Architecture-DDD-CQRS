"use client"

import { motion, useReducedMotion } from "framer-motion"
import * as React from "react"

const ARTISTS = [
    "A. Varela",
    "M. Ito",
    "S. Moreau",
    "N. Chen",
    "E. Duarte",
    "L. Kovács",
    "H. Singh",
    "C. Oliveira",
    "Y. Takahashi",
    "R. Bennett",
]

export default function MarqueeComponent() {
    const prefersReducedMotion = useReducedMotion()

    // Duplicate list to create a seamless loop
    const items = React.useMemo(() => [...ARTISTS, ...ARTISTS], [])

    return (
        <section aria-label="Notable artists" className="relative my-4">
            <div className="pointer-events-none absolute inset-0">
                <div className="absolute inset-x-0 top-0 h-px bg-white/10" />
                <div className="absolute inset-x-0 bottom-0 h-px bg-white/10" />
                <div className="absolute inset-0 bg-[radial-gradient(400px_80px_at_50%_50%,rgba(255,255,255,0.04),transparent_70%)]" />
            </div>

            <div className="relative overflow-hidden py-3">
                <motion.div
                    role="list"
                    aria-label="Artist marquee"
                    className="flex min-w-max items-center gap-10"
                    animate={
                        prefersReducedMotion ? undefined : { x: ["0%", "-50%"] } // move half the duplicated width
                    }
                    transition={
                        prefersReducedMotion ? undefined : { duration: 28, ease: "linear", repeat: Number.POSITIVE_INFINITY }
                    }
                >
                    {items.map((name, i) => (
                        <div role="listitem" key={`${name}-${i}`} className="flex items-center gap-3 text-sm text-muted-foreground">
                            <span className="font-serif text-base text-foreground/90">{name}</span>
                            <span
                                aria-hidden
                                className="h-1 w-1 rounded-full bg-[var(--lux-gold)]/70 ring-2 ring-[var(--lux-gold)]/15"
                            />
                        </div>
                    ))}
                </motion.div>

                <div className="pointer-events-none absolute inset-y-0 left-0 w-24 bg-gradient-to-r from-background to-transparent" />
                <div className="pointer-events-none absolute inset-y-0 right-0 w-24 bg-gradient-to-l from-background to-transparent" />
            </div>
        </section>
    )
}
