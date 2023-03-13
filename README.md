# shopping-list
Simple CRUD app for trying out C# and ASP.NET Core

To deploy it locally, run the docker-compose setup. 

Remember to make sure the file references the local build contexts and not a remote image!

```docker-compose build```

```docker-compose up```

To run it in a kubernetes kluster, reference a built image of the code that is hosted in some repository accessible to the cluster and comment out the local build contexts

Then run below command to generate deployment and service yaml files.

```kompose convert```

Finally, using the generated files, run below kubernetes command towards the cluster to generate the pods and setup 

```kubectl apply -f shopping-app-deployment.yaml,shopping-app-tcp-service.yaml,shopping-db-deployment.yaml,shopping-db-service.yaml```

Finally, grab the external IP of the shopping-app service, seen by running 

```kubectl get services```

Once aquired, try out the setup by running a http request toward the service. For example:

```curl -s -X GET <external-ip>:8080/item```
