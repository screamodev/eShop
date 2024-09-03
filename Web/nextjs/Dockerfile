# Use the official Node.js 18 image as the base image
FROM node:18-alpine as build
COPY . /src
WORKDIR /src/Web/nextjs
COPY **/package.json **/package-lock.json ./
RUN npm install
COPY . .
RUN npm run build

# Use the official Node.js 18 image for production
FROM node:18-alpine as production

# Set the working directory
WORKDIR /app

# Copy built files from the build stage
COPY --from=build /src/Web/nextjs ./

# Install production dependencies
RUN npm install --only=production

# Expose port 3000
EXPOSE 3000

# Run the Next.js application
ENV HOST=0.0.0.0
ENV PORT=3000

# Run the Next.js application
CMD ["npm", "run", "start", "--", "-H", "0.0.0.0"]
