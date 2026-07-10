pipeline {
    agent {
        docker {
            image 'dotnet-sdk-node:9.0'
            args '-u root -v /var/jenkins_home/.nuget/packages:/root/.nuget/packages'
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

        stage('Install Node Packages') {
            steps {
                dir('DiaryPortfolio.Api') {
                    sh 'npm ci'
                }
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
                DiaryPortfolio.Api/DiaryPortfolio.Api.csproj \
                -c Release \
                -o publish
                '''
            }
        }

        // stage('Deploy via FTP') {
        //     steps {
        //         ftpPublisher alwaysPublishFromMaster: false, continueOnError: false, failOnError: true, masterNodeName: '', paramPublish: [parameterName: ""], publishers: [
        //             [
        //                 configName: 'site64986.siteasp.net', 
        //                 transfers: [
        //                     [
        //                         asciiMode: false,
        //                         cleanRemote: false,
        //                         excludes: '',
        //                         flatten: false,
        //                         makeEmptyDirs: false,
        //                         noDefaultExcludes: false,
        //                         patternSeparator: '[, ]+',
        //                         remoteDirectory: 'wwwroot',
        //                         remoteDirectorySDF: false,
        //                         removePrefix: 'publish/', 
        //                         sourceFiles: 'publish/**/*' 
        //                     ]
        //                 ],
        //                 useWorkspaceInPromotion: false,
        //                 verbose: true
        //             ]
        //         ]
        //     }
        // }

        stage('Deploy via Web Deploy') {
            environment {
                IIS_CREDS = credentials('iis-deploy-user') 
            }
            steps {
                sh '''
                msdeploy \
                  -verb:sync \
                  -source:contentPath="publish" \
                  -dest:contentPath="${IIS_SITE_NAME}",computerName="https://${IIS_SERVER}:${IIS_PORT}/msdeploy.axd?site=${IIS_SITE_NAME}",username="${IIS_CREDS_USR}",password="${IIS_CREDS_PSW}",authType="Basic" \
                  -allowUntrusted
                '''
            }
        }

    }
}