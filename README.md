This Unity project aims at mirroring on an avatar the pose of a suject on a photograph.
For now it's an incomplete work in progress. ETA end of August 2018.

Using another of my projects [docker-lifting-from-the-deep](https://github.com/dmaugis/docker-lifting-from-the-deep), which is little more than the docker-ized version of the [Lifting-from the Deep](https://github.com/DenisTome/Lifting-from-the-Deep-release) science work.
## Citation
	@InProceedings{Tome_2017_CVPR,
	author = {Tome, Denis and Russell, Chris and Agapito, Lourdes},
	title = {Lifting From the Deep: Convolutional 3D Pose Estimation From a Single Image},
	booktitle = {The IEEE Conference on Computer Vision and Pattern Recognition (CVPR)},
	month = {July},
	year = {2017}
	}

Let say you get an image from [TJStock](https://www.deviantart.com/tjstock/) on [DevianArt](https://www.deviantart.com/): 

![photo demon_goddess_12 from TJStock on DeviantArt](https://raw.githubusercontent.com/dmaugis/unity-lifting-from-the-deep/master/doc/demon_goddess_12___drawing_pose_reference_by_tjstock-d86cuaq-reduced.jpg "my source photo, demon_goddess_12 / TJStock / DeviantArt")
!!! https://www.deviantart.com/tjstock/journal/Rules-and-Licensing-Agreements-395041532 !!!

Then, using [docker-lifting-from-the-deep](https://github.com/dmaugis/docker-lifting-from-the-deep) you will get the JSON corresponding to the pose as an MPII skeleton (http://human-pose.mpi-inf.mpg.de/):
```json
{'pose3d': [[[-23.66, -41.09, 46.07, 396.54, -25.09, 41.05, 68.55, 13.78, 20.33, -27.06, 83.99, 92.23,-107.04, -105.01, -55.26, -249.51, -165.77], [262572.2, 262689.07, 262699.1, 262735.86, 262455.32, 262491.02, 262517.2, 262574.63, 262558.03, 262530.92, 262549.56, 262438.51, 262314.3, 262382.99, 262685.04, 262766.79, 262658.06], [-154.77, -179.59, -607.06, -491.88, -179.89, -680.34, -1048.2, 106.36, 430.93, 506.17, 689.91, 347.03, 164.34, 372.37, 415.17, 209.76, 377.93]]], 
'pose2d': [[[24, 176], [72, 160], [72, 144], [112, 112], [80, 136], [88, 176], [120, 136], [80, 144], [176, 152], [248, 168], [200, 224], [176, 152], [256, 168], [320, 168]]]}
```
Then, using this JSON with the software in this repository [unity-lifting-from-the-deep](https://github.com/dmaugis/unity-lifting-from-the-deep), the [MakeHuman](http://www.makehumancommunity.org) avatar will mimic the pose in the photograph.

![Alt text](https://github.com/dmaugis/unity-lifting-from-the-deep/blob/master/doc/Screenshot%20from%202018-07-30%2019-58-30.png "Optional title")

As you can see for now it is not complete. The hips are rotated correctly, the hands & feet target a somewhat approximatively correct position, but the head and torso are not oriented correctly.

## Todo
In a few days/weeks :
- head and torso orientation
- use https://github.com/valkjsaaa/Unity-ZeroMQ-Example.git to get the images directly out of [zeromq](http://zeromq.org/) publish.



