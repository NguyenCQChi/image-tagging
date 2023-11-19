This is a [Next.js](https://nextjs.org/) project bootstrapped with [`create-next-app`](https://github.com/vercel/next.js/tree/canary/packages/create-next-app).

## Getting Started

First, run the development server:

```bash
npm run dev
# or
yarn dev
# or
pnpm dev
# or
bun dev
```

Open [http://localhost:3000](http://localhost:3000) with your browser to see the result.
Each page under pages folder can be accessed locally (by http://locahost:3000/[file_name])
Example: for admin.tsx [http://localhost:3000/admin]

## Project Structure
- Most of the code will be under:
  /src/components : for common components that can be reused by different containers.
  /src/containers : for containers that are used directly from the pages under /pages

```
└── root
    ├── pages
    |   ├── api (for API call)
    |   ├── (project routing)
    ├── public
    ├── src
    |   ├── components
    |   ├── constants
    |   ├── containers
    |   |   ├── components (components used by that containers)
    |   ├── contexts
    |   ├── hooks
    |   ├── icons
    |   ├── layouts
    |   ├── stores
    |   ├── styles
    |   ├── types
    |   ├── utils
    ├── authentication (microservice)
```

## Documentation
- [Authentication & Auhtorization Microservice](authentication/README.md)
