rm ./libluajit.a
echo APP_ABI := x86>./Application.mk
NDK=/Users/macbuild/work/android-ndk-r11b
NFK=$NDK/toolchains/x86-4.9/prebuilt/darwin-x86_64/bin/i686-linux-android-

cd luajit/src
make clean
make HOST_CC="gcc -m32 -ffast-math -O3" \
CROSS=$NFK \
TARGET_SYS=Linux \
TARGET_FLAGS="--sysroot $NDK/platforms/android-14/arch-x86 -Wl,--fix-cortex-a8"
cp ./libluajit.a ../../libluajit.a

cd ../../../
ndk-build clean
ndk-build
