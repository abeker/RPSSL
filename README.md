# Rock Paper Scissors Lizard Spock Game

## Description
- This repository contains a microservice implementation of the Rock Paper Scissors Lizard Spock game using C# and .NET Core. The service provides a RESTful API that allows users to play against a computer opponent, following the classic game rules.

## Prerequisites
- Ensure Docker and Docker Compose are installed on your machine. You can download them from [Docker's official website](https://www.docker.com/get-started).

## Installation Instructions
1. Clone the repository: `git clone https://github.com/abeker/RPSSL.git`
2. Navigate to the project directory
3. Run the application using Docker Compose: `docker-compose up --build`

## Usage
- You can access the game through the web interface at `http://localhost`, where you can play against the computer.
- Alternatively, you can use the following API routes:

  - **GET /choice**: This endpoint gives you a random choice from the game.
  
  - **GET /choices**: This returns all the available choices you can make in the game.
  
  - **POST /play**: Use this to play a round against the computer. You need to provide a parameter in the format `{ "player": int }` (where `int` is your choice's ID). The response will tell you who won, along with the choices made:
    - **Results**: Indicates whether you won, lost, or tied.
    - **Player**: Your choice's ID.
    - **Computer**: The computer's choice's ID.

## Features
- Play against a computer opponent through a user-friendly web interface.
- Access a random choice with the **GET /choice** endpoint.
- Retrieve all available game choices using the **GET /choices** endpoint.
- Play a round against the computer with the **POST /play** endpoint.
- Swagger documentation for easy API exploration and understanding.

## Contact Information
For questions or support, please reach out to [Aleksandar Beker](mailto:acabeker@gmail.com).
