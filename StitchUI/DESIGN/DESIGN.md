---
name: Apiary Design System
colors:
  surface: '#121414'
  surface-dim: '#121414'
  surface-bright: '#383939'
  surface-container-lowest: '#0d0e0f'
  surface-container-low: '#1a1c1c'
  surface-container: '#1e2020'
  surface-container-high: '#292a2a'
  surface-container-highest: '#343535'
  on-surface: '#e3e2e2'
  on-surface-variant: '#cfc6ab'
  inverse-surface: '#e3e2e2'
  inverse-on-surface: '#2f3131'
  outline: '#989077'
  outline-variant: '#4c4732'
  surface-tint: '#e7c400'
  primary: '#fff7e6'
  on-primary: '#3a3000'
  primary-container: '#ffd900'
  on-primary-container: '#715f00'
  inverse-primary: '#6f5d00'
  secondary: '#c8c6c5'
  on-secondary: '#303030'
  secondary-container: '#474746'
  on-secondary-container: '#b7b5b4'
  tertiary: '#faf7f7'
  on-tertiary: '#303030'
  tertiary-container: '#dddbda'
  on-tertiary-container: '#616060'
  error: '#ffb4ab'
  on-error: '#690005'
  error-container: '#93000a'
  on-error-container: '#ffdad6'
  primary-fixed: '#ffe165'
  primary-fixed-dim: '#e7c400'
  on-primary-fixed: '#221b00'
  on-primary-fixed-variant: '#544600'
  secondary-fixed: '#e5e2e1'
  secondary-fixed-dim: '#c8c6c5'
  on-secondary-fixed: '#1b1b1c'
  on-secondary-fixed-variant: '#474746'
  tertiary-fixed: '#e4e2e1'
  tertiary-fixed-dim: '#c8c6c5'
  on-tertiary-fixed: '#1b1c1c'
  on-tertiary-fixed-variant: '#474746'
  background: '#121414'
  on-background: '#e3e2e2'
  surface-variant: '#343535'
typography:
  display:
    fontFamily: Hanken Grotesk
    fontSize: 48px
    fontWeight: '700'
    lineHeight: '1.1'
    letterSpacing: -0.02em
  headline-lg:
    fontFamily: Hanken Grotesk
    fontSize: 32px
    fontWeight: '600'
    lineHeight: '1.2'
  headline-lg-mobile:
    fontFamily: Hanken Grotesk
    fontSize: 24px
    fontWeight: '600'
    lineHeight: '1.2'
  body-md:
    fontFamily: Inter
    fontSize: 16px
    fontWeight: '400'
    lineHeight: '1.6'
  body-sm:
    fontFamily: Inter
    fontSize: 14px
    fontWeight: '400'
    lineHeight: '1.5'
  label-caps:
    fontFamily: JetBrains Mono
    fontSize: 12px
    fontWeight: '500'
    lineHeight: '1.0'
    letterSpacing: 0.1em
rounded:
  sm: 0.125rem
  DEFAULT: 0.25rem
  md: 0.375rem
  lg: 0.5rem
  xl: 0.75rem
  full: 9999px
spacing:
  unit: 8px
  container-max: 1200px
  gutter: 24px
  margin-desktop: 64px
  margin-mobile: 20px
---

## Brand & Style

The brand personality is precise, ecological, and industrial. It treats apiculture as both a craft and a science. The design system evokes a sense of "dark-mode functionality," where the deep background represents the interior of a hive or the professional nature of the apiary, while the vibrant yellow accent represents life, honey, and high-visibility activity.

The chosen style is **Minimalist-High Contrast**. It avoids unnecessary ornamentation, relying on the stark juxtaposition between the dark foundation and the luminous accent to guide the user's eye. The interface feels like a premium technical tool—refined enough for luxury honey retail but structured enough for commercial beekeeping management.

## Colors

The palette is strictly controlled to ensure maximum legibility and impact.

- **Background (#1E1E1E):** A deep, matte grey that reduces eye strain and provides a premium "noir" canvas.
- **Primary Accent (#FFD900):** A saturated, high-visibility yellow used exclusively for action-oriented elements and critical information.
- **Surface (#252525):** A slightly lighter shade of grey used to define containers and card backgrounds against the main canvas.
- **Text Tiers:** Pure white (#FFFFFF) for primary headings to ensure 100% contrast, and a muted neutral grey (#A0A0A0) for secondary metadata and helper text.

## Typography

This design system utilizes a trio of sans-serif typefaces to create a functional hierarchy:

- **Hanken Grotesk (Headlines):** Chosen for its sharp, contemporary geometry. It feels engineered and authoritative. Use tight letter-spacing on larger displays to maintain a cohesive visual block.
- **Inter (Body):** The industry standard for digital readability. It ensures that technical descriptions of apiary processes or product details remain legible at all sizes.
- **JetBrains Mono (Labels/Data):** Used for micro-copy, timestamps, and technical data points (e.g., hive temperature, yield metrics). The monospaced nature reinforces the professional, "monitored" feel of the system.

## Layout & Spacing

The layout follows a **Fixed Grid** model for desktop to maintain a cinematic, centered aesthetic, transitioning to a fluid model for smaller viewports.

- **Grid:** A 12-column grid is used for desktop layouts. For mobile, a 4-column grid is standard.
- **Rhythm:** An 8px linear scale governs all padding and margins. 
- **Sectioning:** Use generous vertical padding (80px–120px) between major content sections to emphasize the minimalist "breathing room."
- **Alignment:** Content is generally left-aligned to mimic the structured nature of data logs, though "Display" headers can be centered for landing pages.

## Elevation & Depth

To maintain the clean, minimalist aesthetic, this design system avoids traditional drop shadows. Depth is communicated through **Tonal Layering** and **Low-contrast Outlines**.

- **Level 0 (Background):** #1E1E1E.
- **Level 1 (Cards/Surfaces):** #252525.
- **Borders:** Instead of shadows, use 1px solid borders in #333333 to define boundaries between elements. 
- **Active State:** Use the primary yellow (#FFD900) as a 2px "glow" or border for focused inputs or active cards, rather than increasing elevation.

## Shapes

The shape language is **Soft** but disciplined. 

- **Components:** Standard buttons and input fields use a 0.25rem (4px) radius. This provides a slight hint of approachability while maintaining the overall "technical" feel of the interface.
- **Large Elements:** Product cards or hero image containers use a 0.5rem (8px) radius.
- **Icons:** Should be stroke-based (2px weight) with squared-off ends to match the architectural feel of the typography.

## Components

### Buttons
- **Primary:** Background #FFD900, Text #000000, 4px radius. High-impact for "Order Honey" or "Contact Apiary."
- **Secondary:** Transparent background, 1px Border #FFD900, Text #FFD900.
- **Ghost:** White text with no background, used for secondary navigation or "Cancel" actions.

### Input Fields
- Dark grey fill (#252525) with a 1px border (#333333). On focus, the border turns #FFD900. Labels use the "label-caps" JetBrains Mono style.

### Cards
- No shadow. Flat #252525 background. Borders are 1px #333333. Content should have 24px internal padding.

### Specialized Components
- **Data Badges:** Small rectangular chips using the JetBrains Mono font for displaying hive stats like "Temp: 35°C" or "Status: Active."
- **Progress Bars:** Thin 4px tracks in #333333 with a #FFD900 fill to represent harvest progress or health metrics.
- **Iconography:** Custom thin-line icons representing bees, honeycombs, and botanical elements, always rendered in #FFD900.