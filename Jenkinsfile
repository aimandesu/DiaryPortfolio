pipeline {
    agent {
        docker {
            image 'mcr.microsoft.com/dotnet/sdk:9.0'
            args '-u root'
        }
    }

    stages {

        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restore') {
            steps {
                sh 'dotnet restore DiaryPortfolio.sln'
            }
        }

        stage('Build') {
            steps {
                sh 'dotnet build DiaryPortfolio.sln --configuration Release --no-restore'
            }
        }

        stage('Publish') {
            steps {
                sh '''
                dotnet publish \
                DiaryPortfolio.API/DiaryPortfolio.API.csproj \
                -c Release \
                -o publish
                '''
            }
        }
    }
}