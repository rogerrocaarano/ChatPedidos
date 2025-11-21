#!/bin/bash

# Define the variable name to check
# NOTE: The user's external variable is CLOUDFLARED_TUNNEL_TOKEN, 
# but inside the devcontainer, it might be TUNNEL_TOKEN.
# We will stick to the name provided in the prompt for simplicity.
TOKEN_VAR="CLOUDFLARED_TUNNEL_TOKEN"

TUNNEL_COMMAND="cloudflared tunnel run --token \$${TOKEN_VAR}"

## Function 1: Validate Environment Variable
# Checks if the specified token variable is set and non-empty.
validate_environment_variable() {
    if [ -z "${!TOKEN_VAR}" ]; then
        echo "🚨 ERROR: The environment variable \$$TOKEN_VAR is not set or is empty."
        echo "Please ensure the variable is exported in your shell (e.g., in ~/.bashrc) before launching the container."
        return 1
    else
        echo "✅ Validation successful. \$$TOKEN_VAR is set."
        return 0
    fi
}

## Function 2: Launch Cloudflared Tunnel Service
# Launches the Cloudflare Tunnel using the token.
launch_tunnel_service() {
    echo "🚀 Attempting to launch Cloudflare Tunnel..."
    echo "Executing command: ${TUNNEL_COMMAND}"
    
    # We use 'eval' here to correctly substitute the environment variable at runtime
    eval ${TUNNEL_COMMAND}
    
    # Check the exit status of the cloudflared command
    if [ $? -ne 0 ]; then
        echo "❌ Cloudflared Tunnel failed to start. Check cloudflared logs and configuration."
        return 1
    fi
}

## Main Execution Logic
main() {
    if validate_environment_variable; then
        launch_tunnel_service
    else
        # The validation function already printed the error message
        exit 1
    fi
}

# Execute the main function
main