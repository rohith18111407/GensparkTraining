FROM node:20-alpine AS builder

# Set working directory
WORKDIR /app

# Copy package files and install dependencies
COPY package*.json ./
RUN npm install

# Copy source files and build the Angular app
COPY . .
RUN npm run build --prod

# Stage 2: Serve the built app using Nginx
FROM nginx:stable-alpine

# Copy built Angular app 
COPY --from=builder /app/dist/RazorPayUpiApp/browser /usr/share/nginx/html

# Copy custom nginx config if needed (optional)
# COPY nginx.conf /etc/nginx/nginx.conf

# Expose port
EXPOSE 80

# Start Nginx server
CMD ["nginx", "-g", "daemon off;"]
