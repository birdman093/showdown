#!/bin/bash
set -e

# Start .NET backend in background
cd showdown
if [ -f showdown.sln ]; then
  echo "Starting .NET backend..."
  dotnet run --project backend &
  BACKEND_PID=$!
  echo "Backend running with PID $BACKEND_PID at https://localhost:7240 (or your configured port)"
else
  echo "Solution file not found!"
  exit 1
fi
cd ..

# Start React frontend in foreground
cd showdown/react
if [ -f package.json ]; then
  echo "Starting React frontend..."
  npm startps aux | grep dotnet
else
  echo "package.json not found in react directory!"
  exit 1
fi

# When script exits, kill backend
trap "kill $BACKEND_PID" EXIT
